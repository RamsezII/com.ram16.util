import os

def replace_text_in_file(file_path, old_text, new_text):
    with open(file_path, 'r', encoding='utf-8') as file:
        file_data = file.read()
    
    new_data = file_data.replace(old_text, new_text)
    
    with open(file_path, 'w', encoding='utf-8') as file:
        file.write(new_data)

def traverse_and_replace(directory, old_text, new_text):
    for root, _, files in os.walk(directory):
        for file in files:
            if file.endswith('.cs'):
                file_path = os.path.join(root, file)
                replace_text_in_file(file_path, old_text, new_text)
                print(f'Replaced text in: {file_path}')

if __name__ == "__main__":
    script_directory = os.path.dirname(os.path.realpath(__file__))
    traverse_and_replace(script_directory, '_OLDCORE_', '_NETWORK_')
