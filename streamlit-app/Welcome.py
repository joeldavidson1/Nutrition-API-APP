import streamlit as st
import pandas as pd
import numpy as np
import api_handler
import credentials

st.set_page_config(page_title="Welcome")

st.title('Nutrition')

st.markdown("""
    This is a simple Streamlit application to showcase the use of how the Nutrition API created in C# could be used.
""")

st.session_state.full_response = api_handler.get_full_api_response(credentials.api_key, "foodItems")
st.write(st.session_state.full_response)