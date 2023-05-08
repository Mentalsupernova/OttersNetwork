import requests
from bs4 import BeautifulSoup
from selenium import webdriver

def analyze_page_titl():
    pass


def analyze_headings():
    # Find all heading tags
    headings = soup.find_all(['h1', 'h2', 'h3', 'h4', 'h5', 'h6'])

    # Print the extracted headings
    print("Headings:")
    for heading in headings:
        print(heading.text.strip())


def analyze_text_content():
    # Extract information from the parsed HTML
    # Here's an example of retrieving the page title and the number of paragraphs:
    page_title = soup.title.text.strip()
    # Text Content
    paragraphs = soup.find_all('p')


def analyze_links():
    # Find all anchor tags
    anchors = soup.find_all('a')

    # Extract the URLs from the anchor tags
    links = [anchor['href'] for anchor in anchors if 'href' in anchor.attrs]

    # Print the extracted links
    print("Links:")
    for link in links:
        print(link)


def analyze_images():
    # Find all image tags
    images = soup.find_all('img')

    # Extract the URLs from the image tags
    image_urls = [image['src'] for image in images if 'src' in image.attrs]

    # Print the extracted image URLs
    print("Image URLs:")
    for url in image_urls:
        print(url)


def analyze_forms():
    # Find all form tags
    forms = soup.find_all('form')

    # Extract form information
    for form in forms:
        # Extract form attributes
        form_id = form.get('id')
        form_action = form.get('action')
        form_method = form.get('method')

        # Print form information
        print("Form:")
        print("ID:", form_id)
        print("Action:", form_action)
        print("Method:", form_method)

        # Extract form input fields
        input_fields = form.find_all('input')

        # Print input field information
        print("Input Fields:")
        for input_field in input_fields:
            input_name = input_field.get('name')
            input_type = input_field.get('type')
            print("- Name:", input_name)
            print("  Type:", input_type)

        print()


def analyze_metadata():
    # Find all meta tags
    meta_tags = soup.find_all('meta')

    # Extract metadata information
    metadata = {}
    for meta_tag in meta_tags:
        # Extract meta tag attributes
        meta_name = meta_tag.get('name')
        meta_content = meta_tag.get('content')

        if meta_name and meta_content:
            # Store metadata in a dictionary
            metadata[meta_name] = meta_content

    # Print the extracted metadata
    print("Metadata:")
    for name, content in metadata.items():
        print(f"{name}: {content}")


def analyze_css():
    # Find all meta tags
    meta_tags = soup.find_all('meta')

    # Extract metadata information
    metadata = {}
    for meta_tag in meta_tags:
        # Extract meta tag attributes
        meta_name = meta_tag.get('name')
        meta_content = meta_tag.get('content')

        if meta_name and meta_content:
            # Store metadata in a dictionary
            metadata[meta_name] = meta_content

    # Print the extracted metadata
    print("Metadata:")
    for name, content in metadata.items():
        print(f"{name}: {content}")


def analyze_stylesheets():
    # Find all style tags
    style_tags = soup.find_all('style')

    # Extract stylesheet content from style tags
    stylesheets = [tag.string for tag in style_tags]

    # Find all link tags with rel="stylesheet"
    link_tags = soup.find_all('link', rel='stylesheet')

    # Extract stylesheet URLs from link tags
    stylesheet_urls = [tag['href'] for tag in link_tags]

    # Print the extracted stylesheets
    print("Inline Stylesheets:")
    for stylesheet in stylesheets:
        print(stylesheet)

    print("\nExternal Stylesheet URLs:")
    for url in stylesheet_urls:
        print(url)


def analyze_javaScript_interactions():
    # Configure the WebDriver (you'll need to have the appropriate WebDriver executable)
    driver_chrome = webdriver.Chrome()  # Replace with the path to your WebDriver executable
    driver = webdriver.Chrome(executable_path="/usr/bin/chromedriver/", options=chrome_options )

    # Initialize Firefox/Gecko WebDriver
    driver_firefox = webdriver.Firefox()
    #driver = webdriver.Firefox(executable_path="/usr/bin//", options=firefox_options )

    # Initialize Safari WebDriver
    driver_safari = webdriver.Safari() 
    #driver = webdriver.Safari(executable_path="/usr/bin//", options= )

    # Initialize IE WebDriver
    driver_ie = webdriver.Ie()
    #driver = webdriver.Ie(executable_path="/usr/bin//", options= )

    
    driver_chrome.get(url)

    driver_firefox.get(url)

    driver_safari.get(url)

    driver_ie.get(url) 


    # Extract the JavaScript interactions
    interactions = driver_chrome.execute_script("return performance.getEntriesByType('mark');")

    # Extract the JavaScript interactions
    interactions = driver_firefox.execute_script("return performance.getEntriesByType('mark');")

    # Extract the JavaScript interactions
    interactions = driver_safari.execute_script("return performance.getEntriesByType('mark');")

    # Extract the JavaScript interactions
    interactions = driver.execute_script("return performance.getEntriesByType('mark');")


    # Print the extracted JavaScript interactions
    print("JavaScript Interactions:")
    for interaction in interactions:
        print(interaction)

    # Quit the WebDriver
    driver.quit()


def analyze_website(url):

    # Send a GET request to the website
    response = requests.get(url)

    # Check if the request was successful
    if response.status_code == 200:
        # Parse the HTML content using BeautifulSoup
        soup = BeautifulSoup(response.content, 'html.parser')

        
        

        

        
        

        

        


        

        num_paragraphs = len(paragraphs)


        # Print the results
        print("Website Analysis")
        print("Title:", page_title)
        print("Number of paragraphs:", num_paragraphs)
    else:
        print("Request failed with status code:", response.status_code)

