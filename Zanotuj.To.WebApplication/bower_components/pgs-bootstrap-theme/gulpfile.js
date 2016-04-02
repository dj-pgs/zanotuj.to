var $ = require('gulp-recipe-loader')(require('gulp'), require('require-dir')('gulp-config'), require('gulp-load-plugins'));

$.gulp.task('default', ['build']);


var gulp = require('gulp');

var runTimestamp = Math.round(Date.now()/1000);
var consolidate = require('gulp-consolidate');
gulp.task('generate-icons', function(done){
var async = require('async');
var iconfont = require('gulp-iconfont');
var iconfontCss = require('gulp-iconfont-css');
  var iconStream = gulp.src(['app/assets/svg-src/*.svg'])
    .pipe(iconfontCss({
          fontName: 'pgs-font',
          path: 'app/assets/pgs-font/_icons-template.scss',
          targetPath: '_icons-output.scss',
          fontPath: 'assets/pgs-font/'

      }))

    .pipe(iconfont({
          fontName: 'pgs-font',
    }))
    .pipe(gulp.dest('app/assets/pgs-font/'));

});