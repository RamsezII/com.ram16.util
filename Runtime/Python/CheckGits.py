import os
import subprocess


def check_git_status(repo_path: str):
    os.chdir(repo_path)
    status = {}

    # Check if there are uncommitted changes
    result = subprocess.run(['git', 'status', '--porcelain'], stdout=subprocess.PIPE)
    status['uncommitted_changes'] = bool(result.stdout.strip())

    # Check if the branch is ahead/behind
    result = subprocess.run(['git', 'status', '-sb'], stdout=subprocess.PIPE)
    status_output = result.stdout.decode('utf-8').strip()
    status['branch_status'] = status_output.split('\n')[0] if status_output else "unknown"

    return status


def find_git_repos(base_path: str):
    repos = []
    for root, dirs, files in os.walk(base_path):
        if '.git' in dirs:
            repos.append(root)
            dirs.remove('.git')  # Don't search inside git directories
    return repos


def main(base_path: str):
    git_repos = find_git_repos(base_path)
    for repo in git_repos:
        status = check_git_status(repo)
        print(f"Repository: {repo}")
        print(f"Branch Status: {status['branch_status']}")
        print(f"Uncommitted Changes: {'Yes' if status['uncommitted_changes'] else 'No'}")
        print('---')


if __name__ == "__main__":
    base_path = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))  # Get the directory of the current script
    main(base_path)
