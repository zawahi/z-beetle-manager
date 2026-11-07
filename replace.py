import os
import glob
import re

files = glob.glob('Frontend/src/**/*.vue', recursive=True)

for file in files:
    if 'BaseButton.vue' in file or 'BaseInput.vue' in file:
        continue
    with open(file, 'r') as f:
        content = f.read()
    
    # Replace <button ...> to <BaseButton ...>
    content = re.sub(r'<button\b', r'<BaseButton', content)
    content = re.sub(r'</button>', r'</BaseButton>', content)
    
    # Replace <input ...> to <BaseInput ...>
    content = re.sub(r'<input\b', r'<BaseInput', content)
    
    with open(file, 'w') as f:
        f.write(content)

