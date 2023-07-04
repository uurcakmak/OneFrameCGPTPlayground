/// <binding />

// MANUEL SETUP
// npm i
// npm i -g gulp-cli

// MANUEL RUN
// Open Terminal in this folder and execute command > gulp
// or open 'Task Runner Explorer' window and run with interface

'use strict';
var gulp = require('gulp');
var sass = require('gulp-sass');
var minifyCss = require('gulp-clean-css');
var rename = require('gulp-rename');
var uglify = require('gulp-uglify');

var paths = {
    styles: {
        src: "wwwroot/scss/**/*.scss",
        dest: "wwwroot/scss"
    },
    javascript: {
        src: ["wwwroot/js/**/*.js", "!wwwroot/js/**/*.min.js"],
        dest: "wwwroot/js/"
    }
};

function scssTask() {
    return gulp.src(paths.styles.src)
        .pipe(sass())
        .pipe(gulp.dest(paths.styles.dest))
        .pipe(rename({ suffix: '.min' }))
        .pipe(minifyCss())
        .pipe(gulp.dest(paths.styles.dest));
}

function jsTask() {
    return gulp.src(paths.javascript.src)
        .pipe(uglify())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest(paths.javascript.dest));
}

var build = gulp.series(scssTask, jsTask);

gulp.task('default', build);

gulp.watch(paths.styles.src, gulp.series(scssTask));
gulp.watch(paths.javascript.src, gulp.series(jsTask));
