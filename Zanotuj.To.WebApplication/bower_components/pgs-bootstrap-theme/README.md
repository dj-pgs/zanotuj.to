# PGS Bootstrap Theme

## Examples
To install in project just run:
```
bower install --save  git+https://bitbucket.pgs-soft.com/scm/pfp/pgs-bootstrap-theme.git
```
and import to your project:
```
<script src="bower_components/pgs-bootstrap-theme/dist/scripts.js"></script>
```
```
<link rel="stylesheet" href="bower_components/pgs-bootstrap-theme/dist/assets/fontello/css/fontello.css">
```
```
<link rel="stylesheet" href="bower_components/pgs-bootstrap-theme/dist/assets/fontello/css/fontello-codes.css">
```
```
<link rel="stylesheet" href="bower_components/pgs-bootstrap-theme/dist/assets/fontello/css/animation.css">
```
```
<link rel="stylesheet" href="bower_components/pgs-bootstrap-theme/dist/styles.css">
```

## Checkbox
We are using awesome-bootstrap-checkbox login so everything is based on code convention. Here is checkbox markup from Bootstrap site:

```<form role="form">
  ...
  <div class="checkbox">
    <label>
      <input type="checkbox"> Check me out
    </label>
  </div>
  ...
</form>
```

We have to alter it a bit:

```<form role="form">
  ...
  <div class="checkbox">
    <input type="checkbox" id="checkbox1">
    <label for="checkbox1">
        Check me out
    </label>
  </div>
  ...
</form>```

That's it. It will work. But it will not work if you nest input inside label or put label before input.

If you want to enable Opera 12 and earlier support just add class styled to input element:

```
<input type="checkbox" id="checkbox1" class="styled">
```

