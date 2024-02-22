import requests
import json
import streamlit as st

def get_data_from_api(token: str, get_all_pages: bool = False, **kwargs):
    url = "https://localhost:7099/api/foodItems"
    headers = {"Authorization": f"Bearer {token}"}
    params = kwargs
    
    all_data = []
    while True:
        response = requests.get(url, headers=headers, params=params, verify=False)
        if not response.ok:
            print("Failed to fetch data from the API")
            return None
        
        data = response.json()
        all_data.extend(data)
        
        if not get_all_pages:
            break
        
        pagination_info = json.loads(response.headers.get('x-pagination'))
        if not pagination_info or not pagination_info['HasNext']:
            break
        
        params['PageNumber'] = pagination_info['CurrentPage'] + 1
    
    return all_data