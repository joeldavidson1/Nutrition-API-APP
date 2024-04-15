import requests
import json
import streamlit as st

def get_data_from_api(token: str, end_point: str, get_all_pages: bool = False, **kwargs):
    """
    Retrieves data from an API endpoint.

    Args:
        token (str): The authentication token.
        end_point (str): The API endpoint to retrieve data from.
        get_all_pages (bool, optional): Whether to retrieve data from all pages. Defaults to False.
        **kwargs: Additional query parameters to include in the request.

    Returns:
        list: A list of data retrieved from the API.
    """
    url = f"https://uol-nutrition-api.azurewebsites.net/api/{end_point}"
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
    """
    Retrieves the full API response by calling the get_data_from_api function with the specified token, 
    end_point, and additional keyword arguments.

    Args:
        token (str): The API token.
        end_point (str): The API endpoint.
        **kwargs: Additional keyword arguments to be passed to the get_data_from_api function.

    Returns:
        The full API response.
    """
    return get_data_from_api(token, end_point, True, **kwargs)


def is_authenticated(api_key: str) -> bool:
    """
    Authenticates the API key.

    Args:
        api_key (str): The API key to authenticate.

    Returns:
        bool: True if the API key is valid, False otherwise.
    """
    url = "https://uol-nutrition-api.azurewebsites.net/api/foodItems"
    headers = {"Authorization": f"Bearer {api_key}"}
    response = requests.get(url, headers=headers, verify=False)
    
    if (response.status_code != 200):
        return False
    
    return True