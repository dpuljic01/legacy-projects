import requests
import argparse
from bs4 import BeautifulSoup # I couldn't find good, free API to get list of airports, so I scraped Wikipedia page
# argparser

#OPTIONS = sys.argv[1:] or '--full'

def processOptions(args):
    print("Retrieving data..")
    keys = []
    
    if args.cities:
        keys.append('city')
    if args.names:
        keys.append('names')
    if args.iata:
        keys.append('iata')
    if args.coords:
        keys.append('coords')
    
    if not keys or args.full:
        keys = ['city', 'names', 'iata', 'coords']
    
    airports = getFull(keys)
    print(outputData(keys, airports))

def outputData(keys, airports):
    output = [{key: data[key] for key in keys} for data in airports]
    return output
    
def getFull(keys):
    url = 'https://en.wikipedia.org/wiki/List_of_international_airports_by_country'
    page = requests.get(url)
    soup = BeautifulSoup(page.content, 'html.parser')
    tbls = soup.findAll('table', {'class':'wikitable'})
    uk = tbls[len(tbls) - 2] # get only united kingdom table
    rows = uk.findAll('tr')[2:] # exclude first two weird ones

    airport_info = []
    for row in rows:
        airport = {}
        cols = row('td')
        a,b,c = cols[0], cols[1], cols[2]
        if len(b.text) < 2: continue # skip empty, it happens when region names occur (like Scotland or Wales)
        
        if 'city' in keys: airport['city'] = a.text.strip()
        if 'names' in keys: airport['names'] = b.text.strip()
        if 'iata' in keys: airport['iata'] = c.text.strip()
        if 'coords' in keys: airport['coords'] = getCoords(b.text.strip()) 
        airport_info.append(airport)
    return airport_info

def getCoords(place):
    url = 'https://nominatim.openstreetmap.org/search?'
    res = requests.get(url, {'q': place, 'format': 'json'}).json()
    coords = {'lat': res[0]['lat'], 'lng': res[0]['lon']}
    return coords

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("--cities", help="cities with airports", action="store_true")
    parser.add_argument("--names", help="names of airports", action="store_true")
    parser.add_argument("--coords", help="coordinates of each airport", action="store_true")
    parser.add_argument("--iata", help="IATA codes", action="store_true")
    parser.add_argument("--full", help="all data", action="store_true")

    args = parser.parse_args()
    processOptions(args)
