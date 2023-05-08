import requests



def search_websites_with_prompt(query):
    # Set your OpenAI API key
    api_key = "YOUR_API_KEY"

    # Set the search query and model name
    search_query = f'site:website.com "{query}"'
    model = "text-davinci-002"

    # Generate a completion using the ChatGPT API
    response = requests.post(
        "https://api.openai.com/v1/engines/davinci-codex/completions",
        headers={
            "Authorization": f"Bearer {api_key}",
            "Content-Type": "application/json",
        },
        json={
            "prompt": search_query,
            "model": model,
            "max_tokens": 100,
        },
    )

    # Retrieve the response and print the generated completion
    if response.status_code == 200:
        data = response.json()
        completion = data["choices"][0]["text"].strip()
        print(f"Generated Completion: {completion}")
    else:
        print("Request failed with status code:", response.status_code)

# Example usage
search_query = "How to bake a chocolate cake"
search_websites_with_prompt(search_query) 