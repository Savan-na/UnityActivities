# GitHub Workflow Guide for DECO2300

This guide covers the basic Git workflow you'll use throughout DECO2300 for managing your project changes.

## The Basic Workflow

Every time you work on your project, follow this simple cycle:

1. **Pull** - Get the latest version
2. **Make Changes** - Add new features or fix issues
3. **Commit** - Save your changes with a meaningful message
4. **Push** - Upload your changes to GitHub
5. **Repeat** - Start the cycle again

## Step-by-Step Process

### Step 1: Pull Latest Changes

**Before you start working:**
- Open **GitHub Desktop**
- Click **"Fetch origin"** to check for updates
- If there are changes, click **"Pull origin"**
- This ensures you're working with the most recent version

**Why this matters:**
- Prevents conflicts with changes made on other computers
- Keeps your local copy up-to-date
- Ensures you don't overwrite someone else's work

### Step 2: Make Your Changes

**Create new features or fix issues:**
- **Add new files** - Create new Unity scripts, documentation, etc.
- **Edit existing files** - Modify code, update documentation
- **Delete files** - Remove outdated or unnecessary files
- **Organize folders** - Restructure your project as needed

**Best practices:**
- **Work on one feature at a time** - Keep changes focused
- **Test your changes** - Make sure everything works before committing
- **Use meaningful names** - Name files and folders clearly

### Step 3: Commit Your Changes

**Save your work with a clear message:**
- In GitHub Desktop, you'll see all changed files listed
- **Add a summary** - Brief description of what you changed
- **Add a description** - More detailed explanation (optional)
- Click **"Commit to main"**

**Good commit messages:**
- "Add player movement script"
- "Fix collision detection bug"
- "Update README with setup instructions"

**Bad commit messages:**
- "Fixed stuff"
- "Updated"

### Step 4: Push to GitHub

**Upload your changes:**
- Click **"Push origin"** to upload to GitHub
- Your changes are now live and visible to others
- You can view them on GitHub.com

**What happens:**
- Changes are uploaded to your GitHub repository
- Others can see and download your updates
- Your work is safely backed up online

## Common Scenarios

### Working on a New Feature

```
1. Pull latest changes
2. Create new Unity script
3. Test the script works
4. Commit: "Add player health system"
5. Push changes
```

### Fixing a Bug

```
1. Pull latest changes
2. Find and fix the bug
3. Test the fix works
4. Commit: "Fix player falling through ground"
5. Push changes
```

### Updating Documentation

```
1. Pull latest changes
2. Edit README or documentation
3. Review your changes
4. Commit: "Update project setup instructions"
5. Push changes
```

## Using GitHub Desktop

### Main Interface
- **Changes tab** - Shows files you've modified
- **History tab** - Shows all your commits
- **Repository menu** - Access repository settings

### Key Buttons
- **Fetch origin** - Check for updates
- **Pull origin** - Download latest changes
- **Commit to main** - Save your changes
- **Push origin** - Upload your changes

### File Status
- **Modified** - File has been changed
- **Added** - New file created
- **Deleted** - File removed
- **Renamed** - File name changed

## Best Practices

### Commit Frequently
- **Small, focused commits** - Easier to understand and review
- **Regular saves** - Don't lose hours of work
- **Clear messages** - Explain what and why you changed

### Test Before Committing
- **Unity projects** - Make sure they run without errors
- **Documentation** - Check spelling and grammar
- **Links** - Verify all links work correctly

### Use Descriptive Names
- **Files**: `PlayerMovement.cs`, `GameManager.cs`
- **Folders**: `Scripts/`, `Assets/`, `Documentation/`
- **Commits**: "Add player jumping mechanics"

### Keep It Organized
- **Group related changes** - Commit related files together
- **Use folders** - Organize files logically
- **Update README** - Keep documentation current

## Troubleshooting

### Can't Push Changes?
- **Check internet** - Ensure stable connection
- **Pull first** - Get latest changes before pushing
- **Resolve conflicts** - If files were changed by others

### Changes Not Showing?
- **Check .gitignore** - Files might be ignored
- **Add files manually** - Drag files into GitHub Desktop
- **Refresh repository** - Click "Fetch origin"

### Wrong Commit Message?
- **Edit commit** - Right-click commit, "Edit commit message"
- **Amend commit** - Make changes and commit again

### Lost Changes?
- **Check History** - Look for recent commits
- **Check Recycle Bin** - Files might be there
- **Ask for help** - Contact instructor if needed

## Advanced Tips

### Branching (Optional)
- **Create branches** for major features
- **Merge branches** when features are complete
- **Keep main branch** stable and working

### Collaboration
- **Share repository** with teammates
- **Review changes** before merging
- **Communicate** about what you're working on

### Backup Strategy
- **GitHub** - Primary backup
- **Local copies** - Keep important files elsewhere
- **Cloud storage** - Use Google Drive, OneDrive for large files

## Quick Reference

### Daily Workflow
```
1. Open GitHub Desktop
2. Pull latest changes
3. Make your changes
4. Test everything works
5. Commit with clear message
6. Push to GitHub
```

### Before Starting Work
- ✅ Pull latest changes
- ✅ Check what you're working on
- ✅ Have a clear goal

### After Making Changes
- ✅ Test your changes
- ✅ Write clear commit message
- ✅ Push to GitHub

Remember: This workflow keeps your work safe, organized, and shareable. Practice it regularly! 