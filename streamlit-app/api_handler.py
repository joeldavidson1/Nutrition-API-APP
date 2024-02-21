import requests

def get_data_from_api(token, **kwargs):
    url = "https://localhost:7099/api/foodItems"
    headers = {"Authorization": f"Bearer {token}"}
    params = kwargs
    print(params)
    response = requests.get(url, headers=headers, params=params, verify=False)  # Set verify=False to disable SSL certificate verification
    return response