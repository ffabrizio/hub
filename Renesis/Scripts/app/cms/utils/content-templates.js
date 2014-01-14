define(function (require, exports, module) {
	var settings = {};

	var init = function(s) {
		settings = s;
	};
	
	var getRowHtml = function (item) {
		var tr = '<tr>',
			source = 'Local',
			url = settings.detailsUrl;

		if (item.CultureCode == settings.masterCulture && settings.isLocalInstance) {
			tr = '<tr class="danger">';
			source = 'Master';
			if (settings.isLocalInstance) url = settings.cloneUrl;
		}

		return tr + '<td>' + getItemIconHtml(item.ContentTypeIcon, item) + '<a href="' +
		  url + '/' + item.Id + '">' + item.Name + '</a></td><td><i class="glyphicon glyphicon-time"></i>&nbsp;' +
		  new Date(item.CreationDate).toDateString() + '</td><td>' + source + '</td><td>' +
		  getItemStatusHtml(item) + getItemAdditionalIconsHtml(item) + '</td></tr>';
	};

	var getItemStatusHtml = function (t) {
		if (settings.isComposerMode) {
			var composer = require('cms/content-composer');
			return composer.addModuleControls(t, settings);
		} else {
			if ((t.CultureCode == settings.masterCulture && settings.isLocalInstance) || !t.IsActive) {
				return '<i title="Hidden" class="glyphicon glyphicon-eye-close"></i>&nbsp;';
			}

			return '<i title="Visible" class="glyphicon glyphicon-eye-open"></i>&nbsp;';
		}
	};

	var getItemIconHtml = function (icon, i) {
		return '<i title="' + i.ContentType + '" class="' + icon + '"></i>&nbsp;';
	};

	var getItemAdditionalIconsHtml = function (i) {
		if (!settings.isComposerMode) {
			var v = '';
			if (i.IsFeatured) v += '<i title="Featured" class="glyphicon glyphicon-flash"></i>&nbsp;';
			if (i.IsShared) v += '<i title="Shared across campaigns" class="glyphicon glyphicon-share"></i>&nbsp;';
			return v;
		}

		return '';
	};

	var getContentPreview = function (row) {
		var item = $('a', row),
		    popover = $('<div rel="popover"></div>');
		if ($('[rel="popover"]', row).length === 0) {
			popover.load(item.attr('href') + ' [data-module]', function () {
				row.attr('data-toggle', 'popover')
					.attr('data-content', popover.html());
			});
		}

		row.popover({
			html: true,
			delay: { show: 100, hide: 10 },
			title: 'Preview: ' + item.text(),
			placement: 'bottom',
			container: 'body'
		});
		
		row.on('show.bs.popover', function() {
			$(this).siblings('tr').popover('hide');
		});
	};

	module.exports = {
		init : init,
		getRowHtml: getRowHtml,
		getContentPreview: getContentPreview
	};
});