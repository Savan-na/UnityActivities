# DECO2300 Project File Structure Guide

This guide explains how to organize your DECO2300 project repository for effective development, documentation, and collaboration.

## Reference Structure

For a complete example of recommended project organization, see the [DECO2300 Example Folder Structure](https://github.com/DECO2300-course/ExampleFolderStructure) repository. This example demonstrates best practices for organizing Unity projects, documentation, and prototypes.

## High-Level Folder Organization

### Documentation
Store all your design process, research findings, and evaluation results here. This includes:
- **Design documents** - Game design concepts, mechanics, and systems
- **Research notes** - User research, competitor analysis, technical research
- **Evaluation reports** - User testing results, feedback analysis, iteration notes
- **Process documentation** - Development logs, decision records, meeting notes

### Prototype 1, 2, 3...
Each prototype folder contains:
- **Unity project files** - Complete Unity project for that specific prototype
- **Prototype-specific documentation** - Design rationale, technical decisions, user feedback
- **Assets** - Custom assets created for this prototype

### TestProjects
Store weekly Unity activities and experimental projects here:
- **Weekly activities** - Unity tutorials and exercises from class
- **Experimental features** - Testing new Unity features or techniques
- **Learning projects** - Small projects to practice specific skills
- **Asset experiments** - Testing different art styles, mechanics, or systems

## Git Management Best Practices

### .gitignore Configuration
Always include a proper `.gitignore` file to prevent large files from being committed. Copy the `.gitignore` file from the [Example Folder Structure](https://github.com/DECO2300-course/ExampleFolderStructure) repository to the root of your project.

### Large File Management
**IMPORTANT**: Never commit large files to Git repositories. Instead:
- Use Unity Package Manager for assets
- Store large files externally (Google Drive, OneDrive)
- Use Git LFS for essential large files
- Document external file locations in your README

## README-First Documentation

### Main README.md
Your repository's main README should include:
- **Project overview** - What you're building and why
- **Setup instructions** - How to run your project
- **Development status** - Current progress and next steps
- **Team information** - Contributors and roles
- **Links to key documents** - Design docs, prototypes, etc.

### Folder-Level READMEs
Create README files in each major folder to explain:
- **Purpose** - What belongs in this folder
- **Organization** - How files are structured
- **Usage** - How to work with files in this folder
- **Related links** - Connections to other parts of the project

### Documentation Standards
- **Use Markdown** - Keeps documentation Git-friendly and readable
- **Include screenshots** - Visual documentation is essential for game development
- **Link between documents** - Create a web of connected information
- **Update regularly** - Keep documentation current with your development

## Recommended Project Structure

Follow the structure from the [Example Folder Structure](https://github.com/DECO2300-course/ExampleFolderStructure) repository:

```
YourProject/
├── README.md                    # Main project overview
├── .gitignore                   # Git ignore rules (copy from example repo)
├── Documentation/               # Design and research docs
├── Prototype 1/                # First Unity prototype
├── Prototype 2/                # Second Unity prototype  
├── Prototype 3/                # Final Unity prototype
└── TestProjects/               # Weekly activities and experiments
```

## Getting Started

1. **Fork or clone** the [Example Folder Structure](https://github.com/DECO2300-course/ExampleFolderStructure) repository
2. **Copy the `.gitignore`** file from the example repository to your project root
3. **Follow the folder structure** from the example repository
4. **Create** your main README with project overview
5. **Document** your development process as you go

## Tips for Success

- **Start simple** - Begin with basic structure and expand as needed
- **Be consistent** - Use the same naming and organization patterns throughout
- **Document early** - Write READMEs and documentation as you create folders
- **Update regularly** - Keep your repository structure current with your development

Remember: Good project organization saves time and reduces confusion throughout development. Invest in structure early to reap benefits later!
