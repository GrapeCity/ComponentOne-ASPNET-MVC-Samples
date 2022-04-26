c1.documentReady(function () {
    var tree = wijmo.Control.getControl('#theTree');

    tree.itemsSource = getData();
    tree.displayMemberPath = 'continent,country,city'.split(',');
    tree.childItemsPath = 'countries,cities'.split(',');

    // get the data
    function getData() {
        return [
            {
                continent: 'Africa', countries: [
                  {
                      country: 'Algeria', cities: [
                        { city: 'Algiers' },
                        { city: 'Oran' },
                        { city: 'Constantine' }
                      ]
                  },
                  {
                      country: 'Angola', cities: [
                        { city: 'Luanda' },
                        { city: 'Ambriz' },
                        { city: 'Bailundo' }
                      ]
                  },
                  {
                      country: 'Benin', cities: [
                        { city: 'Porto Novo' },
                        { city: 'Cotonou' },
                        { city: 'Parakou' }
                      ]
                  },
                  {
                      country: 'Botswana', cities: [
                        { city: 'Gaborone' },
                        { city: 'Francistown' },
                        { city: 'Molepolole' }
                      ]
                  },
                ]
            },
            {
                continent: 'Asia', countries: [
                  {
                      country: 'Afghanistan', cities: [
                        { city: 'Kabul' },
                        { city: 'Kandahar' },
                        { city: 'Herat' }
                      ]
                  },
                  {
                      country: 'Armenia', cities: [
                        { city: 'Yerevan' },
                        { city: 'Gyumri' },
                        { city: 'Vanadzor' }
                      ]
                  },
                  {
                      country: 'Azerbaijan', cities: [
                        { city: 'Baku' },
                        { city: 'Agjabadi' },
                        { city: 'Astara' }
                      ]
                  },
                  {
                      country: 'Bahrain', cities: [
                        { city: 'Manama' },
                        { city: 'Riffa' },
                        { city: 'Sitra' }
                      ]
                  },
                ]
            },
            {
                continent: 'Europe', countries: [
                  {
                      country: 'Albania', cities: [
                        { city: 'Tirana' },
                        { city: 'Elbasan' },
                        { city: 'Fier' }
                      ]
                  },
                  {
                      country: 'Andorra', cities: [
                        { city: 'Andorra la Vieja' },
                        { city: 'Canillo' },
                        { city: 'Encamp' }
                      ]
                  },
                  {
                      country: 'Austria', cities: [
                        { city: 'Vienna' },
                        { city: 'Salzburg' },
                        { city: 'Graz' }
                      ]
                  },
                  {
                      country: 'Belarus', cities: [
                        { city: 'Minsk' },
                        { city: 'Barysaw' },
                        { city: 'Slutsk' }
                      ]
                  },
                ]
            },
            {
                continent: 'America', countries: [
                  {
                      country: 'Canada', cities: [
                        { city: 'Ottawa' },
                        { city: 'Toronto' },
                        { city: 'Vancouver' }
                      ]
                  },
                  {
                      country: 'United States', cities: [
                        { city: 'Washington' },
                        { city: 'New York' },
                        { city: 'Pittsburgh' }
                      ]
                  },
                  {
                      country: 'Mexico', cities: [
                        { city: 'Mexico City' },
                        { city: 'Acapulco' },
                        { city: 'San Jose' }
                      ]
                  },
                  {
                      country: 'Belize', cities: [
                        { city: 'Belmopan' },
                        { city: 'Dangriga' },
                        { city: 'Belize City' }
                      ]
                  },
                ]
            },
            {
                continent: 'Ocenania', countries: [
                  {
                      country: 'Australia', cities: [
                        { city: 'Canberra' },
                        { city: 'Sydney' },
                        { city: 'Melbourne' }
                      ]
                  },
                  {
                      country: 'New Zealand', cities: [
                        { city: 'Wellington' },
                        { city: 'Christchurch' },
                        { city: 'Auckland' }
                      ]
                  },
                  {
                      country: 'Fiji', cities: [
                        { city: 'Suva' },
                        { city: 'Labasa' },
                        { city: 'Nasinu' }
                      ]
                  },
                  {
                      country: 'Vanuatu', cities: [
                        { city: 'Port Vila' },
                        { city: 'Forari' },
                        { city: 'Etate' }
                      ]
                  },
                ]
            },
        ];
    }
});