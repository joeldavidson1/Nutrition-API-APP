import streamlit as st
import requests

st.set_page_config(page_title="API testing")

class SessionState:
    def __init__(self, **kwargs):
        self.__dict__.update(kwargs)


def login_to_api(username, password):
    url = "https://localhost:7099/api/authentication/login"
    data = {"userName": username, "password": password}
    # Set verify=False to disable SSL certificate verification
    response = requests.post(url, json=data, verify=False)
    return response

def get_data_from_api(token):
    url = "https://localhost:7099/api/foodItems"
    headers = {"Authorization": f"Bearer {token}"}
    response = requests.get(url, headers=headers, verify=False)  # Set verify=False to disable SSL certificate verification
    return response


def main():
    st.title("API Login")

    # Create or get the session state
    state = SessionState(token="")

    username = st.text_input("Username")
    password = st.text_input("Password", type="password")
    submit_button = st.button("Login")

    if submit_button:
        if username and password:
            response = login_to_api(username, password)
            if response.status_code == 200:
                st.success("Login successful!")
                token = response.json().get("token")
                state.token = token
            else:
                st.error(f"Login failed with status code: {response.status_code}")
        else:
            st.warning("Please enter username and password")

    # Display token if available
    if state.token:
        response = get_data_from_api(state.token)
        if response.status_code == 200:
            st.success("Data retrieved successfully")
            st.write(response.json())
        else:
            st.error("Failed to retrieve data")

    
if __name__ == "__main__":
    main()