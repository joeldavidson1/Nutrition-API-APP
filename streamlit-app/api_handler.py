import requests
import json
import streamlit as st

def get_data_from_api(token: str, end_point: str, get_all_pages: bool = False, **kwargs):
    url = f"https://localhost:7099/api/{end_point}"
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

@st.cache_data
def get_full_api_response(token: str, end_point: str, **kwargs):
   return get_data_from_api(token, end_point, True, **kwargs)