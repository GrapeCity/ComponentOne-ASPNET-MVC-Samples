c1.documentReady(function () {
    // data with some Japanese names in it
    var data = [
          { id: 1, en: "Tokyo", ja: "東京特別区部", pop: 8637098 },
          { id: 2, en: "Yokohama", ja: "横浜市", pop: 3697894 },
          { id: 3, en: "Osaka", ja: "大阪市", pop: 2668586 },
          { id: 4, en: "Nagoya", ja: "名古屋市", pop: 2283289 },
          { id: 5, en: "Sapporo", ja: "札幌市", pop: 1918096 },
          { id: 6, en: "Kobe", ja: "神戸市", pop: 1530847 },
          { id: 7, en: "Kyoto", ja: "京都市", pop: 1474570 },
          { id: 8, en: "Fukuoka", ja: "福岡市", pop: 1430371 },
          { id: 9, en: "Kawasaki", ja: "川崎市", pop: 1373630 },
          { id: 10, en: "Saitama", ja: "さいたま市", pop: 1192418 },
          { id: 11, en: "Hiroshima", ja: "広島市", pop: 1163806 },
          { id: 12, en: "Sendai", ja: "仙台市", pop: 1029552 },
          { id: 13, en: "Kitakyūshū", ja: "北九州市", pop: 986998 },
          { id: 14, en: "Chiba", ja: "千葉市", pop: 938695 },
          { id: 15, en: "Setagaya", ja: "世田谷区", pop: 855416 },
          { id: 16, en: "Sakai", ja: "堺市", pop: 835333 },
          { id: 17, en: "Niigata", ja: "新潟市", pop: 813053 },
          { id: 18, en: "Hamamatsu", ja: "浜松市", pop: 811431 },
          { id: 19, en: "Shizuoka", ja: "静岡市", pop: 710944 },
          { id: 20, en: "Sagamihara", ja: "相模原市", pop: 706342 }
    ];

    var theGrid = wijmo.Control.getControl('#theGrid');
    theGrid.imeEnabled = true;
    theGrid.itemsSource = data;
});