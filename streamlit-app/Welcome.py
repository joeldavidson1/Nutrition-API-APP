import streamlit as st
import pandas as pd
import numpy as np
import api_handler


st.set_page_config(page_title="Welcome")

st.title('UK CoFID Nutrition Showcase App')

st.markdown("""
    This is a simple Streamlit application to showcase the use of how the Nutrition API created in C# could be used. All nutrition data is obtained
    from the API which utilises the UK Composition of Foods Integrated Dataset (CoFID) database. The API is secured using JWT tokens.\n\nTo use the
    pages in the app, please enter your API token. If you do not have a token, please navigate to the API and login or register:
    https://uol-nutrition-api.azurewebsites.net/swagger/index.html
""")

# Check if 'logged_in' key exists in session_state
if 'logged_in' not in st.session_state:
    st.session_state.logged_in = False

# If not logged in, show the login section
if not st.session_state.logged_in:
    st.markdown("### Please enter your API token to use the application")
    api_key = st.text_input("Enter your API key")

    if st.button("Log in"):
        if api_handler.is_authenticated(api_key):
            st.success("Successful login. Collecting full API response for the first time to calculate nutrient percentiles. Please wait...")
            st.session_state.logged_in = True
            st.session_state.api_key = api_key  
            
            st.session_state.full_response = api_handler.get_full_api_response(api_key, "foodItems")
            st.rerun()
            # st.write(st.session_state.full_response)
        else:
            st.error("Invalid API key. Please try again.")
else:
    st.title('Successfully logged in to the API.')
    # st.title('Nutrition')
    # st.write(st.session_state.api_key)
    # Rest of your app code

