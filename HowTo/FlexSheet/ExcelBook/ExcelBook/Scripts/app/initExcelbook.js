'use strict';

$(function () {
	init();
	$(window).on('resize', adjustSize);
});

function init() {
	initRibbon();
	adjustSize();
}

// initialize the button command.
function initRibbon() {
	$('.ribbon-container').find('button[type="button"]').bind('click', function () {
		var cmdText = $(this).children('span.text').text();
		cmdText = cmdText.replace(/ /g, '').toLowerCase();
		exeCmd(cmdText);
	});
}

// execute the corresponding command according to the command text.
function exeCmd(cmdText) {
	switch (cmdText) {
		case 'copy':
			break;
		default:
			break;
	}
}


// adjust the size of the elements in the sample
function adjustSize() {
	var spareHeight = $('.excelbook')[0].clientHeight
		- $('.title')[0].offsetHeight
		- $('.ribbon-container')[0].offsetHeight
		- $('.top-boxes')[0].offsetHeight
		- $('.status')[0].offsetHeight
		- 5;

	$('#flexsheetContainer').height(spareHeight);
}