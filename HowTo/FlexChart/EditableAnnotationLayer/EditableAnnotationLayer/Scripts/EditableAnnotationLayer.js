function createEditableAnnotations(wijmo) {
    var __extends = this.__extends || function (d, b) {
        for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
        function __() { this.constructor = d; }
        __.prototype = b.prototype;
        d.prototype = new __();
    };
    var wijmo;
    (function (wijmo) {
        (function (_chart) {
            (function (annotation) {
                /**
                * Represents a Button of EditableAnnotationLayer.
                */
                var Button = (function () {
                    function Button(iconFunc, drawFunc, resizeFunc) {
                        this._iconFunc = iconFunc;
                        this._drawFunc = drawFunc;
                        this._resizeFunc = resizeFunc;
                    }
                    Object.defineProperty(Button.prototype, "iconFunc", {
                        /**
                        * Gets the function of drawing button icon.
                        */
                        get: function () {
                            return this._iconFunc;
                        },
                        enumerable: true,
                        configurable: true
                    });

                    Object.defineProperty(Button.prototype, "drawFunc", {
                        /**
                        * Gets the function of drawing annotation.
                        */
                        get: function () {
                            return this._drawFunc;
                        },
                        enumerable: true,
                        configurable: true
                    });

                    Object.defineProperty(Button.prototype, "resizeFunc", {
                        /**
                        * Gets the function of resizing annotation.
                        */
                        get: function () {
                            return this._resizeFunc;
                        },
                        enumerable: true,
                        configurable: true
                    });
                    return Button;
                })();
                annotation.Button = Button;

                var TextEditor = (function () {
                    function TextEditor() {
                        /**
                        * Occurs after the chart finishes rendering.
                        */
                        this.clicked = new wijmo.Event();
                        this._create();
                    }
                    Object.defineProperty(TextEditor.prototype, "text", {
                        get: function () {
                            return this._text;
                        },
                        set: function (value) {
                            if (value === this._text) {
                                return;
                            }
                            this._text = value;
                            this._input.value = value;
                        },
                        enumerable: true,
                        configurable: true
                    });

                    Object.defineProperty(TextEditor.prototype, "isShowing", {
                        get: function () {
                            return !!this._isShowing;
                        },
                        enumerable: true,
                        configurable: true
                    });

                    TextEditor.prototype.setFocus = function () {
                        this._input.focus();
                    };

                    //hide TextEditor.
                    TextEditor.prototype.hide = function () {
                        this._element.style.display = 'none';
                        this._isShowing = false;
                    };

                    TextEditor.prototype.show = function (point) {
                        var s = this._element.style;

                        s.display = 'block';
                        this._isShowing = true;
                        if (point) {
                            s.left = point.x + 'px';
                            s.top = point.y + 'px';
                        }
                    };

                    TextEditor.prototype._create = function () {
                        var self = this, ele;

                        ele = document.createElement('div');
                        self._element = ele;
                        ele.style.position = 'absolute';
                        self.hide();

                        self._input = document.createElement('input');
                        ele.appendChild(self._input);

                        self._button = document.createElement('button');
                        self._button.innerHTML = 'OK';
                        self._button.addEventListener('click', self._click.bind(self));
                        ele.appendChild(self._button);
                        document.body.appendChild(ele);
                    };

                    /**
                    * Raises the click event.
                    */
                    TextEditor.prototype.onClicked = function (e) {
                        this.clicked.raise(this, e);
                    };

                    TextEditor.prototype._click = function (e) {
                        this._text = this._input.value || '';
                        this.onClicked(new wijmo.EventArgs());
                    };
                    return TextEditor;
                })();

                /**
                * Class that represents a point with data (with x and y coordinates and dx, dy).
                */
                var DataPoint = (function (_super) {
                    __extends(DataPoint, _super);
                    /**
                    * Initializes a new instance of a @see:DataPoint object.
                    *
                    * @param x X coordinate of the new DataPoint.
                    * @param y Y coordinate of the new DataPoint.
                    */
                    function DataPoint(x, y) {
                        if (typeof x === "undefined") { x = 0; }
                        if (typeof y === "undefined") { y = 0; }
                        _super.call(this, x, y);
                    }
                    return DataPoint;
                })(wijmo.Point);
                annotation.DataPoint = DataPoint;

                /**
                * Represents an extension of the editable annotation layer for the FlexChart.
                *
                * The EditableAnnotationLayer contains a collection of annotation elements: texts, lines, images, rectangle etc.
                */
                var EditableAnnotationLayer = (function (_super) {
                    __extends(EditableAnnotationLayer, _super);
                    /**
                    * Initializes a new instance of a @see:EditableAnnotationLayer object.
                    *
                    * @param chart The chart on which the EditableAnnotationLayer attached to.
                    * @param options A JavaScript object containing initialization data for EditableAnnotationLayer.
                    */
                    function EditableAnnotationLayer(chart, options) {
                        _super.call(this, chart, options);
                        this._hostMouseDown = null;
                        this._hostMouseMove = null;
                        this._hostMouseUp = null;
                        this._hostMouseLeave = null;
                        this._hostClick = null;
                        this._hostDBLClick = null;
                        this._isDragging = false;

                        //bind drag/drop event
                        this._bindEditableEvent();
                    }
                    EditableAnnotationLayer.prototype._init = function (chart) {
                        _super.prototype._init.call(this, chart);

                        var self = this;

                        self._textEditor = new TextEditor();
                        self._textEditor.clicked.addHandler(self._textEditorClick, self);
                        self._buttons = new wijmo.collections.ObservableArray();

                        var text, textBtn = new Button(function (engine) {
                            var textIcon = new annotation.Text({
                                text: 'T',
                                tooltip: 'Text Button</br>Select to add Text Annotation.',
                                point: { x: 10, y: 22 }
                            });
                            textIcon.render(engine);
                            return textIcon._element;
                        }, function (point, isDataCoordinate) {
                            var x = isDataCoordinate ? point.dx : point.x, y = isDataCoordinate ? point.dy : point.y;

                            text = new annotation.Text({
                                attachment: isDataCoordinate ? 1 /* DataCoordinate */ : 3 /* Absolute */,
                                tooltip: 'Text',
                                text: 'Text',
                                point: { x: x, y: y },
                                style: {
                                    fill: 'black'
                                }
                            });
                            self.items.push(text);
                        }, function (originPoint, currentPoint) {
                        });
                        self.buttons.push(textBtn);

                        var line, lineBtn = new Button(function (engine) {
                            var lineIcon = new annotation.Line({
                                position: 0 /* Center */,
                                tooltip: 'Line Button</br>Select to add Line Annotation.',
                                start: { x: 5, y: 6 },
                                end: { x: 15, y: 15 }
                            });
                            lineIcon.render(engine);
                            return lineIcon._element;
                        }, function (point, isDataCoordinate) {
                            var x = isDataCoordinate ? point.dx : point.x, y = isDataCoordinate ? point.dy : point.y;

                            line = new annotation.Line({
                                attachment: isDataCoordinate ? 1 /* DataCoordinate */ : 3 /* Absolute */,
                                position: 0 /* Center */,
                                tooltip: 'Line',
                                start: { x: x, y: y },
                                end: { x: x, y: y }
                            });
                            self.items.push(line);
                        }, function (originPoint, currentPoint, isDataCoordinate) {
                            var ox = isDataCoordinate ? originPoint.dx : originPoint.x, oy = isDataCoordinate ? originPoint.dy : originPoint.y, cx = isDataCoordinate ? currentPoint.dx : currentPoint.x, cy = isDataCoordinate ? currentPoint.dy : currentPoint.y;

                            line.start = new wijmo.chart.DataPoint(ox, oy);
                            line.end = new wijmo.chart.DataPoint(cx, cy);
                        });
                        self.buttons.push(lineBtn);

                        var circle, circleBtn = new Button(function (engine) {
                            var circleIcon = new annotation.Circle({
                                tooltip: 'Circle Button</br>Select to add Circle Annotation.',
                                point: { x: 10, y: 11 },
                                radius: 6,
                                style: { fill: 'red' }
                            });
                            circleIcon.render(engine);
                            return circleIcon._element;
                        }, function (point, isDataCoordinate) {
                            var x = isDataCoordinate ? point.dx : point.x, y = isDataCoordinate ? point.dy : point.y;

                            circle = new annotation.Circle({
                                attachment: isDataCoordinate ? 1 /* DataCoordinate */ : 3 /* Absolute */,
                                tooltip: 'Circle',
                                point: { x: x, y: y },
                                radius: 0,
                                style: {
                                    fill: 'red'
                                }
                            });
                            self.items.push(circle);
                        }, function (originPoint, currentPoint) {
                            circle.radius = Math.sqrt(Math.pow(currentPoint.x - originPoint.x, 2) + Math.pow(currentPoint.y - originPoint.y, 2));
                        });
                        self.buttons.push(circleBtn);

                        var ellipse, ellipseBtn = new Button(function (engine) {
                            var ellipseIcon = new annotation.Ellipse({
                                tooltip: 'Ellipse Button</br>Select to add Ellipse Annotation.',
                                point: { x: 10, y: 11 },
                                width: 16,
                                height: 10,
                                style: { fill: 'red' }
                            });
                            ellipseIcon.render(engine);
                            return ellipseIcon._element;
                        }, function (point, isDataCoordinate) {
                            var x = isDataCoordinate ? point.dx : point.x, y = isDataCoordinate ? point.dy : point.y;

                            ellipse = new annotation.Ellipse({
                                attachment: isDataCoordinate ? 1 /* DataCoordinate */ : 3 /* Absolute */,
                                tooltip: 'Ellipse',
                                point: { x: x, y: y },
                                width: 0,
                                height: 0,
                                style: {
                                    fill: 'red'
                                }
                            });
                            self.items.push(ellipse);
                        }, function (originPoint, currentPoint) {
                            ellipse.width = Math.abs(currentPoint.x - originPoint.x) * 2;
                            ellipse.height = Math.abs(currentPoint.y - originPoint.y) * 2;
                        });
                        self.buttons.push(ellipseBtn);

                        var rect, rectBtn = new Button(function (engine) {
                            var rectangleIcon = new annotation.Rectangle({
                                tooltip: 'Rectangle Button</br>Select to add Rectangle Annotation.',
                                point: { x: 10, y: 11 },
                                width: 14,
                                height: 10,
                                style: { fill: 'red' }
                            });
                            rectangleIcon.render(engine);
                            return rectangleIcon._element;
                        }, function (point, isDataCoordinate) {
                            var x = isDataCoordinate ? point.dx : point.x, y = isDataCoordinate ? point.dy : point.y;

                            rect = new annotation.Rectangle({
                                attachment: isDataCoordinate ? 1 /* DataCoordinate */ : 3 /* Absolute */,
                                tooltip: 'Rectangle',
                                point: { x: x, y: y },
                                width: 0,
                                height: 0,
                                style: {
                                    fill: 'red'
                                }
                            });
                            self.items.push(rect);
                        }, function (originPoint, currentPoint) {
                            rect.width = Math.abs(currentPoint.x - originPoint.x) * 2;
                            rect.height = Math.abs(currentPoint.y - originPoint.y) * 2;
                        });
                        self.buttons.push(rectBtn);

                        var square, squareBtn = new Button(function (engine) {
                            var squareleIcon = new annotation.Square({
                                tooltip: 'Square Button</br>Select to add Square Annotation.',
                                point: { x: 10, y: 11 },
                                length: 10,
                                style: { fill: 'red' }
                            });
                            squareleIcon.render(engine);
                            return squareleIcon._element;
                        }, function (point, isDataCoordinate) {
                            var x = isDataCoordinate ? point.dx : point.x, y = isDataCoordinate ? point.dy : point.y;

                            square = new annotation.Square({
                                attachment: isDataCoordinate ? 1 /* DataCoordinate */ : 3 /* Absolute */,
                                tooltip: 'Square',
                                point: { x: x, y: y },
                                length: 0,
                                style: {
                                    fill: 'red'
                                }
                            });
                            self.items.push(square);
                        }, function (originPoint, currentPoint) {
                            square.length = Math.abs(currentPoint.x - originPoint.x) * 2;
                        });
                        self.buttons.push(squareBtn);
                    };

                    Object.defineProperty(EditableAnnotationLayer.prototype, "buttons", {
                        /**
                        * Gets the collection of annotation elements in AnnotationLayer.
                        */
                        get: function () {
                            return this._buttons;
                        },
                        enumerable: true,
                        configurable: true
                    });

                    EditableAnnotationLayer.prototype._renderGroup = function () {
                        var self = this;

                        _super.prototype._renderGroup.call(this);

                        //create editable UI
                        if (!self._editableUI || self._editableUI.parentNode == null) {
                            self._createEditableUI();
                        }
                    };

                    EditableAnnotationLayer.prototype._createEditableUI = function () {
                        var self = this, engine = self._chart._currentRenderEngine;

                        if (!self._editableUI || self._editableUI.parentNode == null) {
                            if (!self._editableUI) {
                                self._chartMarginPlot = self._chart.plotMargin;
                            }

                            self._editableUI = engine.startGroup('wjanno-editable-layer');
                            engine.endGroup();

                            self._drawIcons(engine);
                        }
                        self._toggleUIVisible(self._isEditable);
                    };

                    //draw rounded corner container
                    EditableAnnotationLayer.prototype._drawRCContainer = function (engine, x) {
                        if (typeof x === "undefined") { x = 0; }
                        engine.drawRect(x, 1, 20, 20, 'wjanno-container', {
                            rx: 2,
                            ry: 2
                        });
                    };

                    EditableAnnotationLayer.prototype._drawIcons = function (engine) {
                        var self = this, ele = self._editableUI, btns = self.buttons, idx, len, btn, iconGroup, icon, iconOffset = 65, iconSelectedClass = ' wjanno-icon-selected', cls;

                        while (ele.hasChildNodes()) {
                            ele.removeChild(ele.lastChild);
                        }

                        //data coordinate
                        cls = 'wjanno-dc-icon wjanno-icon';
                        if (self._isDataCoordinate) {
                            cls += iconSelectedClass;
                        }
                        var dc = engine.startGroup(cls);
                        self._drawRCContainer(engine, 5);
                        engine.endGroup();
                        var dcIcon = new annotation.Polygon({
                            tooltip: 'Data Coordinate Button</br> Click to switch the attachment of the annotation to be added</br> between Absolute and DataCoordinate.',
                            points: [
                                {
                                    x: 16, y: 5
                                }, {
                                    x: 10, y: 5
                                }, {
                                    x: 10, y: 17
                                }, {
                                    x: 20, y: 17
                                }, {
                                    x: 20, y: 9
                                }]
                        });
                        dcIcon.render(engine);
                        dc.appendChild(dcIcon._element);
                        ele.appendChild(dc);

                        //delete
                        cls = 'wjanno-delete-icon wjanno-icon';
                        if (self._isDeleting) {
                            cls += iconSelectedClass;
                        }
                        var del = engine.startGroup(cls);
                        self._drawRCContainer(engine, 35);
                        engine.endGroup();
                        var deleteIcon = new annotation.Text({
                            text: 'X',
                            tooltip: 'Delete Button </br> Click to enable delete mode on selecting an annotation on the chart.',
                            point: { x: 45, y: 22 }
                        });
                        deleteIcon.render(engine);
                        del.appendChild(deleteIcon._element);
                        ele.appendChild(del);

                        for (idx = 0, len = btns.length; idx < len; idx++) {
                            btn = btns[idx];
                            cls = 'wjanno-icon';
                            if (self._selectedBtnIdx === idx) {
                                cls += iconSelectedClass;
                            }
                            iconGroup = engine.startGroup(cls);
                            self._drawRCContainer(engine);
                            engine.endGroup();
                            iconGroup.setAttribute('transform', 'translate(' + iconOffset + ', 0)');

                            icon = btn.iconFunc.call(self, engine);
                            if (icon) {
                                iconGroup.appendChild(icon);
                            }

                            iconOffset += 30;
                            ele.appendChild(iconGroup);
                        }
                    };

                    EditableAnnotationLayer.prototype._toggleUIVisible = function (value) {
                        var self = this, str = value ? 'visible' : 'hidden', margin;

                        if (!self._editableUI) {
                            self._createEditableUI();
                        }

                        self._editableUI.setAttribute('visibility', str);
                        if (value) {
                            if (!self._topMarginAdded) {
                                margin = self._convertMargin();
                                self._chart.plotMargin = margin;
                                self._topMarginAdded = true;
                            }
                        } else {
                            self._chart.plotMargin = self._chartMarginPlot;
                            self._topMarginAdded = false;
                        }
                    };

                    EditableAnnotationLayer.prototype._convertMargin = function () {
                        var chart = this._chart, margin = chart._parseMargin(chart.plotMargin), m = [];

                        if (margin.top) {
                            margin.top = +margin.top + 35;
                        } else {
                            margin.top = 35;
                        }
                        m.push(margin.top, margin.right ? margin.right : NaN, margin.bottom ? margin.bottom : NaN, margin.left ? margin.left : NaN);
                        return m.join(' ');
                    };

                    EditableAnnotationLayer.prototype._bindEditableEvent = function () {
                        var self = this;

                        self._hostMouseDown = self._onHostMouseDown.bind(self);
                        self._hostMouseMove = self._onHostMouseMove.bind(self);
                        self._hostMouseUp = self._onHostMouseUp.bind(self);
                        self._hostMouseLeave = self._onHostMouseLeave.bind(self);
                        self._hostClick = self._onHostClick.bind(self);
                        self._hostDBLClick = self._onHostDBLClick.bind(self);

                        self.toggleDragEventBinding();
                    };

                    EditableAnnotationLayer.prototype.toggleDragEventBinding = function (bind) {
                        if (typeof bind === "undefined") { bind = true; }
                        var self = this, ele = self._chart.hostElement, eventListener = bind ? 'addEventListener' : 'removeEventListener';

                        ele[eventListener]('mousedown', self._hostMouseDown);
                        ele[eventListener]('mousemove', self._hostMouseMove);
                        ele[eventListener]('mouseup', self._hostMouseUp);
                        ele[eventListener]('mouseleave', self._hostMouseLeave);
                        ele[eventListener]('click', self._hostClick);
                        ele[eventListener]('dblclick', self._hostDBLClick);

                        //prevent default drag behavior of image.
                        ele[eventListener]('dragstart', function (e) {
                            if (e.target && e.target.tagName && e.target.tagName.toLowerCase() === 'image') {
                                e.preventDefault();
                                return false;
                            }
                        });

                        if ('ontouchstart' in window) {
                            ele[eventListener]('touchstart', self._hostMouseDown);
                            ele[eventListener]('touchmove', self._hostMouseMove);
                            ele[eventListener]('touchend', self._hostMouseUp);
                        }
                    };

                    EditableAnnotationLayer.prototype._getIconElement = function (ele, pNode) {
                        var parentNode = ele.parentNode;
                        if (wijmo.hasClass(ele, 'wjanno-icon')) {
                            return ele;
                        } else if (parentNode == null || parentNode === document.body || parentNode === document || parentNode === pNode) {
                            return null;
                        } else {
                            return this._getIconElement(parentNode, pNode);
                        }
                    };

                    EditableAnnotationLayer.prototype._onHostDBLClick = function (e) {
                        var self = this, rootEle = self._chart.hostElement, anno, pNode;

                        if (!self._isEditable) {
                            return;
                        }
                        anno = self._getAnnotation(e.target, rootEle);
                        if (anno != null) {
                            pNode = anno._element.parentNode;
                            if (pNode == self._layerEle) {
                                self._dlbClick(e, anno);
                            }
                        }
                    };

                    EditableAnnotationLayer.prototype._textEditorClick = function (e) {
                        var self = this, textEditor = self._textEditor, anno = self._textEditorAnno, text;

                        if (!self._isEditable) {
                            return;
                        }
                        text = textEditor.text;
                        textEditor.hide();
                        if (anno instanceof annotation.Text) {
                            anno.text = text;
                        } else {
                            anno.content = text;
                        }
                        self._textEditorAnno = null;
                    };

                    EditableAnnotationLayer.prototype._dlbClick = function (e, anno) {
                        var self = this, textEditor = self._textEditor, text;

                        if (!self._isEditable) {
                            return;
                        }
                        if (anno instanceof annotation.Text) {
                            text = anno.text || '';
                        } else {
                            text = anno.content || '';
                        }

                        self._textEditorAnno = anno;
                        textEditor.text = text;
                        textEditor.show(new wijmo.Point(e.pageX, e.pageY));
                        textEditor.setFocus();
                    };

                    EditableAnnotationLayer.prototype._onHostClick = function (e) {
                        var self = this, tar = e.target, iconEle, selectedIcon, iconSelectedClass = 'wjanno-icon-selected', isDC = self._isDataCoordinate;

                        if (!self._isEditable) {
                            return;
                        }
                        iconEle = self._getIconElement(tar, self._editableUI);
                        if (!iconEle) {
                            return;
                        }

                        //for data-coordinate icon only
                        if (wijmo.hasClass(iconEle, 'wjanno-dc-icon')) {
                            self._isDataCoordinate = !isDC;
                            wijmo.toggleClass(iconEle, iconSelectedClass, !isDC);
                            return;
                        }

                        self._isDeleting = false;
                        self._selectedBtnIdx = -1;
                        if (wijmo.hasClass(iconEle, iconSelectedClass)) {
                            wijmo.removeClass(iconEle, iconSelectedClass);
                        } else {
                            selectedIcon = self._editableUI.querySelector('.' + iconSelectedClass + ':not(:first-child)');
                            if (selectedIcon != null) {
                                wijmo.removeClass(selectedIcon, iconSelectedClass);
                            }
                            wijmo.addClass(iconEle, iconSelectedClass);
                            if (wijmo.hasClass(iconEle, 'wjanno-delete-icon')) {
                                self._isDeleting = true;
                            } else {
                                self._selectedBtnIdx = [].indexOf.call(iconEle.parentNode.childNodes, iconEle) - 2;
                            }
                        }
                    };

                    EditableAnnotationLayer.prototype._onHostMouseDown = function (e) {
                        var self = this, rootEle = self._chart.hostElement, anno, pt = e instanceof MouseEvent ? new wijmo.Point(e.pageX, e.pageY) : new wijmo.Point(e.changedTouches[0].pageX, e.changedTouches[0].pageY), pNode, del, btn, hostRect, plotRect = self._chart._plotRect, isDC = self._isDataCoordinate, dp;

                        if (!self._isEditable) {
                            return;
                        }
                        rootEle.style.cursor = 'default';

                        self._toggleIsAdding(false);
                        self._originPoint = pt;
                        anno = self._getAnnotation(e.target, rootEle);
                        if (anno != null) {
                            pNode = anno._element.parentNode;
                            if (pNode == self._layerEle) {
                                if (self._isDeleting) {
                                    del = window.confirm('Delete selected Annotation?');
                                    if (del) {
                                        self.items.removeAt(self.items.indexOf(anno));
                                    }
                                    return;
                                }
                                self._toggleIsDragging(true, anno);

                                // move this element to the "top" of the display, so it is (almost) always over other elements
                                anno._element.parentNode.appendChild(anno._element);
                                if (self._chart.isTouching) {
                                    self._timer = window.setTimeout(self._dlbClick(e, anno), 1000);
                                }
                            }
                        } else if (self._selectedBtnIdx > -1) {
                            hostRect = wijmo.getElementRect(rootEle.querySelector('svg'));
                            pt = new wijmo.Point(pt.x - hostRect.left, pt.y - hostRect.top);
                            if (!_chart.FlexChart._contains(plotRect, pt)) {
                                return;
                            }

                            //inside plot rect
                            //check whether is adding new annotation
                            self._toggleIsAdding(true);
                            btn = self._buttons[self._selectedBtnIdx];

                            dp = new DataPoint(pt.x - plotRect.left, pt.y - plotRect.top);
                            if (isDC) {
                                self._convertPtToData(dp);
                            }
                            btn.drawFunc.call(self, dp, isDC);
                            e.preventDefault();
                        }
                    };

                    EditableAnnotationLayer.prototype._convertPtToData = function (point) {
                        var self = this, chart = self._chart, rect = chart._plotRect;

                        point.dx = self._convertLenToData(rect.width, chart.axisX, point.x);
                        point.dy = self._convertLenToData(rect.height, chart.axisY, point.y, true);
                    };

                    EditableAnnotationLayer.prototype._convertLenToData = function (total, axis, val, converted) {
                        if (typeof converted === "undefined") { converted = false; }
                        var min = axis.min == null ? axis.actualMin : axis.min, max = axis.max == null ? axis.actualMax : axis.max, isDate = wijmo.isDate(min), v;

                        if (isDate) {
                            //val = FlexChart._fromOADate(val);
                            min = _chart.FlexChart._toOADate(min);
                            max = _chart.FlexChart._toOADate(max);
                        }
                        if (converted) {
                            v = (1 - val / total) * (max - min) + min;
                        } else {
                            v = val / total * (max - min) + min;
                        }
                        if (isDate) {
                            v = _chart.FlexChart._fromOADate(v);
                        }
                        return v;
                    };

                    EditableAnnotationLayer.prototype._clearTimer = function () {
                        if (this._timer) {
                            window.clearTimeout(this._timer);
                            this._timer = null;
                        }
                    };

                    EditableAnnotationLayer.prototype._onHostMouseMove = function (e) {
                        var self = this, rootEle = self._chart.hostElement, tarAnno = self._draggingAnno, oriPt = self._originPoint, newPt, plotRect = self._chart._plotRect, btn, hostRect, isDC = self._isDataCoordinate, odp, cdp;

                        if (!self._isEditable) {
                            return;
                        }
                        self._clearTimer();
                        if (!oriPt) {
                            return;
                        }
                        newPt = e instanceof MouseEvent ? new wijmo.Point(e.pageX, e.pageY) : new wijmo.Point(e.changedTouches[0].pageX, e.changedTouches[0].pageY);

                        if (self._isDragging) {
                            tarAnno._element.setAttribute('transform', 'translate(' + (newPt.x - oriPt.x) + ',' + (newPt.y - oriPt.y) + ')');
                        } else if (self._isAdding) {
                            hostRect = wijmo.getElementRect(rootEle.querySelector('svg'));
                            oriPt = new wijmo.Point(oriPt.x - hostRect.left, oriPt.y - hostRect.top);
                            newPt = new wijmo.Point(newPt.x - hostRect.left, newPt.y - hostRect.top);

                            //check whether is adding new annotation
                            btn = self._buttons[self._selectedBtnIdx];

                            odp = new DataPoint(oriPt.x - plotRect.left, oriPt.y - plotRect.top);
                            cdp = new DataPoint(newPt.x - plotRect.left, newPt.y - plotRect.top);
                            if (isDC) {
                                self._convertPtToData(odp);
                                self._convertPtToData(cdp);
                            }
                            btn.resizeFunc.call(self, odp, cdp, isDC);
                        }
                    };

                    EditableAnnotationLayer.prototype._onHostMouseUp = function (e) {
                        var self = this;

                        if (!self._isEditable) {
                            return;
                        }
                        self._clearTimer();
                        if (self._isDragging) {
                            self._convertTransform();
                            self._toggleIsDragging(false);
                        }
                        if (self._isAdding) {
                            self._toggleIsAdding(false);
                        }
                        delete self._chart.hostElement.style.cursor;
                    };

                    EditableAnnotationLayer.prototype._onHostMouseLeave = function (e) {
                        this._onHostMouseUp(e);
                    };

                    EditableAnnotationLayer.prototype._convertTransform = function () {
                        //because it is a sample, so convert transform to offset only.
                        var anno = this._draggingAnno, ele = anno._element, offset = new wijmo.Point(), translate, plotRect, transform;

                        transform = ele.getAttribute('transform');

                        //IE - translate(0)
                        if (transform == null || transform === 'translate(0)') {
                            return;
                        }
                        transform = transform.replace('translate(', '').replace(')', '');
                        if (transform.indexOf(',') > -1) {
                            translate = transform.split(',').map(function (v) {
                                return +v;
                            });
                        } else {
                            translate = transform.split(' ').map(function (v) {
                                return +v;
                            });
                        }

                        //if (anno.attachment === AnnotationAttachment.DataCoordinate) {
                        //    plotRect = this._chart._plotRect;
                        //    offset.x = anno.offset.x + translate[0] / plotRect.width;
                        //    offset.y = anno.offset.y + translate[1] / plotRect.height;
                        //    anno.
                        //} else {
                        offset.x = anno.offset.x + translate[0];
                        offset.y = anno.offset.y + translate[1];
                        anno.offset = offset;

                        //}
                        ele.removeAttribute('transform');
                    };

                    EditableAnnotationLayer.prototype._toggleIsDragging = function (value, anno) {
                        var self = this, svg = this._layerEle.parentNode;

                        if (value) {
                            self._isDragging = true;
                            wijmo.addClass(self._chart.hostElement, 'no-selection');
                            if (anno) {
                                self._draggingAnno = anno;
                            }
                        } else {
                            self._isDragging = false;
                            wijmo.removeClass(self._chart.hostElement, 'no-selection');
                            self._draggingAnno = null;
                        }
                    };

                    EditableAnnotationLayer.prototype._toggleIsAdding = function (value) {
                        var self = this, svg = this._layerEle.parentNode;

                        if (value) {
                            self._isAdding = true;
                            wijmo.addClass(self._chart.hostElement, 'no-selection');
                        } else {
                            self._isAdding = false;
                            wijmo.removeClass(self._chart.hostElement, 'no-selection');
                        }
                    };

                    Object.defineProperty(EditableAnnotationLayer.prototype, "isEditable", {
                        /**
                        * Gets or sets the visibility of the Annotation.
                        */
                        get: function () {
                            return !!this._isEditable;
                        },
                        set: function (value) {
                            if (value === this._isEditable) {
                                return;
                            }
                            this._isEditable = wijmo.asBoolean(value, false);
                            this._toggleUIVisible(value);
                        },
                        enumerable: true,
                        configurable: true
                    });
                    return EditableAnnotationLayer;
                })(annotation.AnnotationLayer);
                annotation.EditableAnnotationLayer = EditableAnnotationLayer;
            })(_chart.annotation || (_chart.annotation = {}));
            var annotation = _chart.annotation;
        })(wijmo.chart || (wijmo.chart = {}));
        var chart = wijmo.chart;
    })(wijmo || (wijmo = {}));
    //# sourceMappingURL=EditableAnnotationLayer.js.map
};