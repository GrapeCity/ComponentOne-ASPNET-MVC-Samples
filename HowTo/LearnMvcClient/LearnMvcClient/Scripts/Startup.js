function initTabs() {
    SyntaxHighlighter.all();
    SyntaxHighlighter.defaults['toolbar'] = false;
    SyntaxHighlighter.defaults['quick-code'] = false;
    var titles = document.getElementsByClassName('collapse-title'),
        sampleNavBtn = document.getElementById('sampleNavBtn');

    createTabs('navList', 'panelList');
    createTabs('sourceTab', 'sourcePanel');
}

function initHamburgerNav() {
    var hamburgerNavBtn = document.querySelector('.hamburger-nav-btn');
    var hamburgerNavEle = document.querySelector('.narrow-screen.site-nav');
    new c1.sample.MultiLevelMenu(hamburgerNavEle, hamburgerNavBtn);
}

function initTaskNav() {
    var taskNavBtn = document.querySelector('.task-bar>.semi-bold.narrow-screen');
    var taskNavEle = document.querySelector('.narrow-screen.lesson-nav');
    new c1.sample.DropDownMenu(taskNavEle, taskNavBtn);
}

c1.documentReady(function () {
    initTabs();
    initHamburgerNav();
    initTaskNav();
});