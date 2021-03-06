const router = require('express').Router();

router.use('/auth', require('./auth'));
router.use('/accounts', require('./accounts'));
router.use('/quests', require('./quests'));
router.use('/badges', require('./badges'));

// eslint-disable-next-line
router.use((err, req, res, next) => {
    if (err.userError) {
        return res.status(400).json({
            error: err.name,
            message: err.message
        });
    }

    if (err.name === 'UnauthorizedError') {
        return res.status(403).json({
            error: err.name,
            message: err.message
        });
    }

    // eslint-disable-next-line
    console.log(err);
    return res.status(500).json({
        error: 'ServerError',
        message: err.message ? `Error detail: ${err.message}` : 'A problem has occurred, try again later'
    });
});

module.exports = router;
