define(function (require, exports, module) {
	'use strict';
	
	var Backbone = require('backbone');
	
	module.exports = Backbone.Router.extend({
		routes: {
			':culture/:campaign/admin': 'list',
			':culture/:campaign/admin/index': 'list',
			':culture/:campaign/admin/details/:id': 'details',
			':culture/:campaign/admin/create/:id': 'edit',
			':culture/:campaign/admin/edit/:id': 'edit',
			':culture/:campaign/admin/clone/:id': 'edit',
			':culture/:campaign/admin/delete/:id': 'remove',
			'': 'default',
			'*id': 'default'
		},
		initialize: function () {

		},
		list: function (culture, campaign) {
			//console.log('admin list', campaign);

			var AdminListView = require('views/admin/list');
			var v = new AdminListView();
			v.render();
		},
		details: function (culture, campaign, id) {
			//console.log('admin details ' + id, campaign);
		},
		edit: function (culture, campaign, id) {
			//console.log('admin edit ' + id, campaign);

			var AdminEditView = require('views/admin/edit');
			var v = new AdminEditView();
			v.render();
		},
		remove: function (culture, campaign, id) {
			//console.log('admin delete ' + id, campaign);

			var AdminDeleteView = require('views/admin/delete');
			var v = new AdminDeleteView();
			v.render();
		},
		default: function () {
			//console.log('default');

			var DefaultView = require('views/admin/list');
			var v = new DefaultView();
			v.render();
		}
	});

});