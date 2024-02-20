import streamlit as st
import requests

st.set_page_config(page_title="API testing")


def login_to_api(username, password):
    url = "https://localhost:7099/api/authentication/login"
    data = {"userName": username, "password": password}
    response = requests.post(url, json=data, verify=False)

    return response


# st.title("API Tests")

# username = st.text_input("Username")
# password = st.text_input("Password", type="password")
# submit_bu