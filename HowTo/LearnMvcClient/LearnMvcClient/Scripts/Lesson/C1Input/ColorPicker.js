c1.documentReady(function () {
    // ColorPicker
    var theColorPicker = wijmo.Control.getControl('#theColorPicker');
    theColorPicker.valueChanged.addHandler(function (s, e) {
        setBackground(s.value);
    });

    // Custom ColorPicker
    var theColorPickerCst = wijmo.Control.getControl('#theColorPickerCst');
    theColorPickerCst.valueChanged.addHandler(function (s, e) {
        setBackground(s.value);
    });

    // customization
    document.getElementById('showAlphaChannel').addEventListener('click', function (e) {
        theColorPickerCst.showAlphaChannel = e.target.checked;
    });
    document.getElementById('showColorString').addEventListener('click', function (e) {
        theColorPickerCst.showColorString = e.target.checked;
    });
    var thePalette = wijmo.Control.getControl('#thePalette');
    thePalette.itemsSource = getPalettes();
    thePalette.selectedIndexChanged.addHandler(function (s, e) {
        theColorPickerCst.palette = s.selectedValue.colors;
    });

    // show the color that was selected
    function setBackground(color) {
        document.getElementById('output').style.background = color;
        theColorPicker.value = color;
        theColorPickerCst.value = color;
    }
  
    // color palettes
    function getPalettes() {
        return [
          { name: 'Standard', colors: ['#fff', '#000', '#FFBE00', '#FFFF00', '#94D752', '#00B652', '#00B6EF', '#0075C6', '#002263', '#73359C'] },
          { name: 'Office', colors: ['#fff', '#000', '#15487B', '#EFEFE7', '#4A82BD', '#C6504A', '#9CBA5A', '#8465A5', '#4AAEC6', '#F79642'] },
          { name: 'GrayScale', colors: ['#fff', '#000', '#000000', '#FFFFFF', '#DEDEDE', '#B4B4B4', '#969696', '#828282', '#5A5A5A', '#4B4B4B'] },
          { name: 'Apex', colors: ['#fff', '#000', '#6B656B', '#CEC3D6', '#CEBA63', '#9CB284', '#6BB2CE', '#6386CE', '#7B69CE', '#A578BD'] },
          { name: 'Aspect', colors: ['#fff', '#000', '#332E33', '#E7DFD6', '#F77D00', '#382733', '#15597B', '#4A8642', '#63487B', '#C69A5A'] },
          { name: 'Civic', colors: ['#fff', '#000', '#636984', '#C6D3D6', '#D6604A', '#CEB600', '#28AEAD', '#8C7873', '#8CB28C', '#0E924A'] },
          { name: 'Concourse', colors: ['#fff', '#000', '#424442', '#DEF7FF', '#2BA2BD', '#DE1C2B', '#EF6515', '#38609C', '#42487B', '#7B3D4A'] },
          { name: 'Equity', colors: ['#fff', '#000', '#6B6563', '#EFE7DE', '#D64815', '#9C2B15', '#A58E6B', '#946052', '#948684', '#845D5A'] },
          { name: 'Flow', colors: ['#fff', '#000', '#00607B', '#DEF7FF', '#006DC6', '#009EDE', '#00D3DE', '#15CF9C', '#7BCB63', '#A5C34A'] },
          { name: 'Foundry', colors: ['#fff', '#000', '#636952', '#EFEBDE', '#73A273', '#B5CFB5', '#ADCFD6', '#C6BEAD', '#CEC794', '#EFB6B5'] },
          { name: 'Median', colors: ['#fff', '#000', '#735D52', '#EFDFC6', '#94B6D6', '#DE8242', '#A5AA84', '#DEB25A', '#7BA69C', '#948E8C'] },
          { name: 'Metro', colors: ['#fff', '#000', '#4A596B', '#D6EFFF', '#7BD338', '#EF157B', '#FFBA00', '#00AEDE', '#738ACE', '#15B29C'] },
          { name: 'Module', colors: ['#fff', '#000', '#5A607B', '#D6D7D6', '#F7AE00', '#63B6CE', '#E76D7B', '#6BB66B', '#EF8652', '#C64842'] },
          { name: 'Opulent', colors: ['#fff', '#000', '#B53D9C', '#F7E7EF', '#BD3D6B', '#AD65BD', '#DE6D33', '#FFB638', '#CE6DA5', '#FF8E38'] },
          { name: 'Oriel', colors: ['#fff', '#000', '#525D6B', '#FFF39C', '#FF8633', '#739ADE', '#B52B15', '#F7CF2B', '#ADBAD6', '#737D84'] },
          { name: 'Origin', colors: ['#fff', '#000', '#424452', '#DEEBEF', '#737DA5', '#9CBACE', '#D6DB7B', '#FFDB7B', '#BD8673', '#8C726B'] },
          { name: 'Paper', colors: ['#fff', '#000', '#424C22', '#FFFBCE', '#A5B694', '#F7A642', '#E7BE2B', '#D692A5', '#9C86C6', '#849EC6'] },
          { name: 'Solstice', colors: ['#fff', '#000', '#4A2215', '#E7DFCE', '#3892A5', '#FFBA00', '#C62B2B', '#84AA33', '#944200', '#42598C'] },
          { name: 'Technic', colors: ['#fff', '#000', '#383838', '#D6D3D6', '#6BA2B5', '#CEAE00', '#8C8AA5', '#738663', '#9C9273', '#7B868C'] },
          { name: 'Trek', colors: ['#fff', '#000', '#4A3833', '#FFEFCE', '#F7A22B', '#A5654A', '#B58A84', '#C69A6B', '#A59673', '#C6752B'] },
          { name: 'Urban', colors: ['#fff', '#000', '#424452', '#DEDFDE', '#52558C', '#428284', '#A54CA5', '#C6652B', '#8C5D38', '#5A92B5'] },
          { name: 'Verve', colors: ['#fff', '#000', '#636563', '#D6D3D6', '#FF388C', '#E7005A', '#9C007B', '#6B007B', '#0059D6', '#00359C'] },

          { name: 'standard', colors: ['#88bde6', '#fbb258', '#90cd97', '#f6aac9', '#bfa554', '#bc99c7', '#eddd46', '#f07e6e', '#8c8c8c'] },
          { name: 'cocoa', colors: ['#466bb0', '#c8b422', '#14886e', '#b54836', '#6e5944', '#8b3872', '#73b22b', '#b87320', '#141414'] },
          { name: 'coral', colors: ['#84d0e0', '#f48256', '#95c78c', '#efa5d6', '#ba8452', '#ab95c2', '#ede9d0', '#e96b7d', '#888888'] },
          { name: 'dark', colors: ['#005fad', '#f06400', '#009330', '#e400b1', '#b65800', '#6a279c', '#d5a211', '#dc0127', '#000000'] },
          { name: 'highcontrast', colors: ['#ff82b0', '#0dda2c', '#0021ab', '#bcf28c', '#19c23b', '#890d3a', '#607efd', '#1b7700', '#000000'] },
          { name: 'light', colors: ['#ddca9a', '#778deb', '#778deb', '#b5eae2', '#7270be', '#a6c7a7', '#9e95c7', '#95b0c7', '#9b9b9b'] },
          { name: 'midnight', colors: ['#83aaca', '#e37849', '#14a46a', '#e097da', '#a26d54', '#a584b7', '#d89c54', '#e86996', '#2c343b'] },
          { name: 'minimal', colors: ['#92b8da', '#e2d287', '#accdb8', '#eac4cb', '#bbbb7a', '#cab1ca', '#cbd877', '#dfb397', '#c8c8c8'] },
          { name: 'modern', colors: ['#2d9fc7', '#ec993c', '#89c235', '#e377a4', '#a68931', '#a672a6', '#d0c041', '#e35855', '#68706a'] },
          { name: 'organic', colors: ['#9c88d9', '#a3d767', '#8ec3c0', '#e9c3a9', '#91ab36', '#d4ccc0', '#61bbd8', '#e2d76f', '#80715a'] },
          { name: 'slate', colors: ['#7493cd', '#f99820', '#71b486', '#e4a491', '#cb883b', '#ae83a4', '#bacc5c', '#e5746a', '#505d65'] }
        ];
    }
});