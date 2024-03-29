module.exports = {
    aliases: {
        "@node_modules": "./node_modules",
        "@libs": "./wwwroot/libs"
    },
    clean: [
        "@libs"
    ],
    mappings: {
        "@node_modules/bootswatch/dist/**/*.css": "@libs/bootswatch/",
        "@node_modules/chart.js/dist/*": "@libs/chart.js/"
    }
};
