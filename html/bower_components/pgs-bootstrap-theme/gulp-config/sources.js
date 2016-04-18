var paths = require('./paths');
var _ = require('lodash');
var sources = {};

sources.defaultBase = paths.app;

// dev serving
sources.index = paths.app + 'index.html';

// files handled by sass recipe
sources.sass = [
    paths.app + 'components/**/*.{sass,scss}',
    paths.app + '*.{sass,scss}'
];

// files handled by css recipe
sources.css = [
    paths.app + 'components/**/*.css',
    paths.app + 'assets/fontello/css/*.css',
    paths.app + 'assets/css/*.css',
    paths.app + '*.css'
];

// split files into variables by categories
var js = [
    paths.app + 'components/**/*.js',
    paths.app + '*.js'
];

var bowerFiles = {
    files: [
        paths.app + 'bower_components/*/*.js',
        paths.app + 'bower_components/*/{dist,min,release}/*.{js,css}',// most of the generic bower modules
        paths.app + 'bower_components/*/{dist,min,release}/js/standalone/*.{js,css}',// selectize
        paths.app + 'bower_components/bootstrap-sass/assets/javascripts/bootstrap.js'
    ],
    watch: false
};

// Files not handled by other tasks that need to be passed into pipemin (possibly referenced in index.html).
// If you don't handle css or js by other tasks, pass them here.
sources.assets = [bowerFiles, js, [paths.app + 'assets/fontello/font/fontello.eot']];

// Files not handled by other tasks that need to be included into build, bypassing pipemin entirely.
// These files are also watched in development.
sources.build = [
    paths.app + '*.css',
    paths.app + 'assets/**/*',
];

module.exports = sources;