// TODO: const { createUserError } = require('../../utils');
const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const mongooseMerge = require('mongoose-merge-plugin');
const mongooseBCrypt = require('mongoose-bcrypt');
const mongooseTimestamp = require('mongoose-timestamp');

const Account = new Schema({
    connection: {
        email: {
            type: String,
            validate: {
                validator: (v) => {
                    // eslint-disable-next-line
                    return /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/.test(v);
                }
            },
            unique: true,
            required: true
        },
        password: {
            type: String,
            required: true,
            bcrypt: true
        },
    },
    userInformation: {
        lastName: String,
        firstName: String,
        username: {
            type: String,
            required: true
        }, // Pseudo
        accountType: {
            type: String,
            enum: {
                values: ['ADMIN', 'CREATOR', 'USER'],
                message: '`accountType` field must be one of these value [\'ADMIN\', \'CREATOR\', \'USER\']'
            },
            default: 'USER'
        }
    },
    dates: {
        createdAt: Date, // Auto
        updatedAt: Date // Auto

    },
    game: {
        badges: [Schema.Types.ObjectId], // Badges ObjectId Array
        quests: [
            {
                _idQuest: Schema.Types.ObjectId, // Quest ObjectId
                state: {
                    type: String,
                    enum: ['IN_PROGRESS', 'DONE'],
                    default: 'IN_PROGRESS'
                },
                stats: {
                    earnedXp: Number
                }
            }
        ],
        xp: Number,
        elapsedTime: Number // Second
    }
});

Account.plugin(mongooseMerge);
Account.plugin(mongooseBCrypt);
Account.plugin(mongooseTimestamp);

module.exports = mongoose.model('Account', Account);
