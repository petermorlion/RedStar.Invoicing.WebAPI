var gulp = require("gulp"),
   babel = require("gulp-babel")
      fs = require("fs");

var aureliaSrc = 'aurelia/src';
var aureliaDist = 'aurelia/dist';

gulp.task("babel", function () {
    gulp.src([aureliaSrc + '/**/*.js'])
      .pipe(babel({
          "optional": ["runtime"],
          "stage": 0,
          'modules': 'system'
      }))
      .pipe(gulp.dest(aureliaDist));
});

gulp.task('build-html', function () {
    gulp.src([aureliaSrc + '/**/*.html'])
      .pipe(gulp.dest(aureliaDist));
});