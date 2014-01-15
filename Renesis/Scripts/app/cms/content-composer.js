define(function (require, exports, module) {

	'use strict';

	var settings = {};

	var init = function (s) {
		$('a.navbar-brand', '#filters').hide();

		settings = s;
		setupPickers();
		setupSave();
		
	};

	var setupPickers = function () {
		var picker = $('<a data-toggle="modal" href="#content-picker" class="btn btn-primary"><i class="glyphicon glyphicon-th"></i>&nbsp;Manage group items</a>');
		picker.on('click', function () {
			var unavailableSettings = ['Group'];

			var type = $('input[name="ContentType"]').val();
			var parentRow = $('i[title="' + type + '"]', settings.results);

			if (unavailableSettings.indexOf(type) > -1) {
				parentRow.parents('tr').remove();
			}
		});
		
		settings.composerArea.after(picker);
	};

	var setupSave = function () {
		var form = $('form'),
		    btn = $('button#submitBtn', 'form');

		btn.on('click', function (e) {
			e.preventDefault();

			var contentModules = $('div[data-module]', settings.composerArea);
			if (contentModules.length) {
				var items = [];
				contentModules.each(function () {
					items.push($(this).data('module'));
				});

				$('input[name="ContentItemsReferences"]', form).val(items.join(','));
			}
		
			form.submit();
		});
	};

	var manageItems = function (area) {
		$('a[data-action="#manage-module"]', settings.results).each(function () {
			getButtonState($(this));
		});
		
		$('a[data-action="#manage-module"]', settings.results).on('click', function (e) {
			e.preventDefault();

			var button = $(this),
				newEl = $('[data-module="' + $(this).attr('rel') + '"]', area);

			if (newEl.length === 0) {
				$('<div />').load(button.attr('href') + ' [data-module]', function () {
					area.prepend($(this));
					getButtonState(button, true);
				});

			} else {
				newEl.remove();
				getButtonState(button, false);
			}
		});
	};

	var buttonStates = ['<i class="glyphicon glyphicon-plus"></i>', '<i class="glyphicon glyphicon-minus"></i>'];
	
	var getButtonState = function (el, a) {
		if (a === false) {
			el.html(buttonStates[0]);
		} else if (a === true) {
			el.html(buttonStates[1]);
		} else {
			var m = $('[data-module="' + el.attr('rel') + '"]', a).parent();
			if (!m.length) {
				el.html(buttonStates[0]);
			} else {
				el.html(buttonStates[1]);
			}
		}

	};

	var addModuleControls = function (t) {
		if ((t.CultureCode == settings.masterCulture && !settings.isMaster) || !t.IsActive) {
			return '<i class="glyphicon glyphicon-eye-close"></i> Hidden';
		}

		return '<a href="' + settings.detailsUrl + '/' + t.Id + '" data-action="#manage-module" rel="' + t.Id + '" class="btn btn-primary">' + buttonStates[0] + '</i></a>';
	};

	module.exports = {
		init: init,
		addModuleControls: addModuleControls,
		manageItems: manageItems
	};
});