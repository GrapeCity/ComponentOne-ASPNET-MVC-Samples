function addClass(obj, cls) {
    if (obj && !this.hasClass(obj, cls)) obj.className += ' ' + cls;
}

function hasClass(obj, cls) {
    return obj && obj.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));
}

function toggleTabPanelVisibility(tabs, panels, i) {
    var activeID = 0, j;
    for (j = 0; j < tabs.length; j++) {
        if (tabs[j].parentElement.className.indexOf('active') >= 0) {
            activeID = j;
            break;
        }
    }
    if (activeID != i) {
        removeClass(tabs[activeID].parentElement, 'active');
        removeClass(panels[activeID], 'active');
        addClass(tabs[i].parentElement, 'active');
        addClass(panels[i], 'active');
    }
}

function removeClass(obj, cls) {
    if (obj && hasClass(obj, cls)) {
        var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
        obj.className = obj.className.replace(reg, ' ');
    }
}

function getAutomaticHeight(ele) {
    var name = '__AutomaticHeight';
    if (!ele[name]) {
        var html = ele.outerHTML;
        var copy = $(html);
        copy.width(0);
        copy.css('height', 'auto');
        copy.css('display', '');
        copy.appendTo(document.body);
        ele[name] = copy.height() + 'px';
        copy.remove();
    }
    return ele[name];
}

function slideDown(ele, refer) {
    ele.style.height = '0px';
    wijmo.showPopup(ele, refer);
    $(ele).animate({ height: getAutomaticHeight(ele) });
}
function slideUp(ele) {
    $(ele).animate({ height: '0px' }, undefined, function () {
        wijmo.hidePopup(ele);
        ele.style.height = 'auto';
    });
}

function eventFrom(event, element) {
    return element && (element.contains(event.target) || event.target === element);
}

function findLesson(list, controllerName, actionName, id) {
    if (list && controllerName) {
        for (var i = 0; i < list.length; i++) {
            var lesson = list[i];
            var lActionName = lesson.ActionName.toLowerCase();
            var lControllerName = lesson.ControllerName.toLowerCase();
            if (lControllerName == controllerName.toLowerCase()
                 && lActionName == actionName.toLowerCase()
                 && lesson.Id == id) {
                return lesson;
            }

            if (lesson.SubLessons) {
                var tlesson = findLesson(lesson.SubLessons, controllerName, actionName, id);
                if (tlesson != null) {
                    return tlesson;
                }
            }
        }
    }
}