const base64Img = require('base64-img');
const uuidv4 = require('uuid-v4');
const fs = require('fs');
const Quest = require('../models/quest.js');
const { createUserError, createServerError } = require('../../helpers/errors');

async function getAll() {
    return Quest.find();
}

async function getQuestById(questId) {
    let quest = await Quest.findById(questId);
    quest = quest.toJSON();
    if (!quest) throw createUserError('Unknow quest', 'No quest found with the provided _id.');
    quest.picture = await base64EncodeToPromise(quest.picturePath);
    delete quest.picturePath;
    for (const checkpoint of quest.checkpoints) {
        quest.checkpoints[quest.checkpoints.indexOf(checkpoint)].picture = await base64EncodeToPromise(checkpoint.picturePath);
        delete quest.checkpoints[quest.checkpoints.indexOf(checkpoint)].picturePath;
    }
    return quest;
}

async function createQuest(quest, questPicture, checkpoints) {
    // Quest picture
    const name = uuidv4();
    const filePath = await base64DecodeToPromise(questPicture, 'pictures', name);
    if (!filePath) throw createServerError('ServerError', 'Picture decode/write failed.');
    quest.picturePath = filePath;

    // Checkpoint pictures
    for (const checkpoint of checkpoints) {
        const name = uuidv4();
        const filePath = await base64DecodeToPromise(checkpoint.picture, 'pictures', name);
        if (!filePath) throw createServerError('ServerError', 'Picture decode/write failed.');
        quest.checkpoints[checkpoints.indexOf(checkpoint)].picturePath = filePath;
        delete checkpoint.picture;
    }

    return quest.save();
}

async function updateQuest(questId, questModif) {
    const actualQuest = await Quest.findById(questId);

    if (questModif.picture) {
        const oldPicturePath = actualQuest.picturePath;
        const name = uuidv4();
        const filePath = await base64DecodeToPromise(questModif.picture, 'pictures', name);
        if (!filePath) throw createServerError('ServerError', 'Picture decode/write failed.');
        actualQuest.picturePath = filePath;
        await new Promise(resolve => fs.unlink(oldPicturePath, res => resolve(res)));
    }

    if (questModif.checkpoints) {
        const oldCheckpointsPicturePath = actualQuest.checkpoints.map(checkpoint => checkpoint.picturePath);
        for (const checkpoint of questModif.checkpoints) {
            const name = uuidv4();
            const filePath = await base64DecodeToPromise(checkpoint.picture, 'pictures', name);
            if (!filePath) throw createServerError('ServerError', 'Picture decode/write failed.');
            actualQuest.checkpoints[questModif.checkpoints.indexOf(checkpoint)].picturePath = filePath;
        }
        for (const picturePath of oldCheckpointsPicturePath) {
            await new Promise(resolve => fs.unlink(picturePath, res => resolve(res)));
        }
    }

    actualQuest.merge(questModif);

    return actualQuest.save();
}

async function deleteQuest(questId) {
    const quest = await getQuestById(questId);

    await new Promise(resolve => fs.unlink(quest.picturePath, res => resolve(res)));
    for (const picturePath of quest.checkpoints.map(checkpoint => checkpoint.picturePath)) {
        await new Promise(resolve => fs.unlink(picturePath, res => resolve(res)));
    }

    const condition = { _id: questId };

    return Quest.remove(condition);
}

function base64DecodeToPromise(imgData, dest, name) {
    return new Promise((resolve, reject) => {
        base64Img.img(imgData, dest, name, (err, response) => {
            if (err) reject(err);
            resolve(response);
        });
    });
}

function base64EncodeToPromise(path) {
    return new Promise((resolve, reject) => {
        base64Img.base64(path, (err, response) => {
            if (err) reject(err);
            resolve(response);
        });
    });
}

module.exports = {
    getAll,
    getQuestById,
    createQuest,
    updateQuest,
    deleteQuest,
};
