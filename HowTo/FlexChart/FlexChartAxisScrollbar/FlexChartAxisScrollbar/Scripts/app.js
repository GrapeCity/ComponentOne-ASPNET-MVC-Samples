var flexChart = null;
var axisXScrollbar, axisYScrollbar;

function initialControls() {
    flexChart = wijmo.Control.getControl("#flexChart");    
    setAxisScrollbars();
};

function setAxisScrollbars() {
    if (!flexChart) {
        return;
    }
    flexChart.plotMargin = 'NaN NaN NaN 80';
    // create Scrollbar
    if (!axisXScrollbar) {
        axisXScrollbar = new wijmo.chart.interaction.AxisScrollbar(flexChart.axes[0], {
            minScale: 0.02
        });
    }

    if (!axisYScrollbar) {
        axisYScrollbar = new wijmo.chart.interaction.AxisScrollbar(flexChart.axes[1], {
            buttonsVisible: false,
            minScale: 0.05
        });
    }
};

