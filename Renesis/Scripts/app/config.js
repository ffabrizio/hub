require.config({
	urlArgs: 'bust=' + (new Date()).getTime(),
	paths: {
		'underscore': '../underscore.min',
		'jquery': '../jquery-2.0.3.min',
		'jq-validate': '../jquery.validate.min',
		'jq-validate-unobtrusive': '../jquery.validate.unobtrusive.min',
		'backbone': '../backbone.min',
		'text': '../text',
		'bootstrap': '../bootstrap.min',
		'datepicker': '../bootstrap-datepicker',
		'tinymce': '../tinymce/tinymce.min',
	},

	shim: {
		'backbone': {
			deps: ['jquery', 'underscore'],
			exports: 'Backbone'
		},
		'bootstrap': {
			deps: ['jquery'],
			exports: 'bootstrap'
		},
		'tinymce': {
			exports: 'tinymce',
			init: function () {
				this.tinyMCE.DOM.events.domLoaded = true;
				return this.tinyMCE;
			}
		}
	}
});