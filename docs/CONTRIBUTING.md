# Contributing to Sophia

## Building the project locally

To generate local `.dll` files to the `./lib/` folder run:

```powershell
./BuildProject.ps1
```

A `.unitypackage` of the `.dll` files build can be created with the following script:

```powershell
./BuildUnityPackages.ps1
```


## Submitting changes

Please send a [GitHub Pull Request](https://github.com/xrlabdevelopment/Sophia/pulls) with a clear list of what you've done (read more about [pull requests](http://help.github.com/pull-requests/)).
Please follow our commit message conventions (below) and make sure all of your commits are atomic (one feature per commit).

## Commit Message Format

We have very precise rules over how our Git commit messages must be formatted.
This format leads to **easier to read commit history** and enables us to use the commit messages to generate the **change log of the project**. We follow Anglular's message format.

Each commit message consists of a **header**, a **body**, and a **footer**.


```
<header>
<BLANK LINE>
<body>
<BLANK LINE>
<footer>
```

The `header` is mandatory and must conform to the **Commit Message Header** format found below

The `body` is recommended for all commits except for those of scope "docs".
When the body is required it must be at least 20 characters long.

The `footer` is optional.

Any line of the commit message cannot be longer than 100 characters.


#### Commit Message Header

```
<type>(<scope>): <short summary>
  │       │             │
  │       │             └─⫸ Capitalized short summary. No period at the end.
  │       │
  │       └─⫸ Commit Scope: core|editor|platform|libraries|
  │                          packaging|changelog|
  │
  └─⫸ Commit Type: build|ci|docs|feat|fix|perf|refactor|style|test
```

The `<type>` and `<summary>` fields are mandatory, the `(<scope>)` field is optional.


##### Type

Must be one of the following:

* **build**: Changes that affect the build system or external dependencies ((example scopes: msbuild, xUnit)
* **ci**: Changes to our CI configuration files and scripts (example scopes: AppVeyor, xUnit)
* **docs**: Documentation only changes
* **feat**: A new feature
* **fix**: A bug fix
* **perf**: A code change that improves performance
* **refactor**: A code change that neither fixes a bug nor adds a feature
* **style**: Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc)
* **test**: Adding missing tests or correcting existing tests

#### Commit Message Body

Explain the motivation for the change in the commit message body. This commit message should explain _why_ you are making the change.
You can include a comparison of the previous behaviour with the new behaviour in order to illustrate the impact of the change.


#### Commit Message Footer

The footer can contain information about breaking changes and is also the place to reference GitHub issues and other PRs that this commit closes or is related to.

```
BREAKING CHANGE: <breaking change summary>
<BLANK LINE>
<breaking change description + migration instructions>
<BLANK LINE>
<BLANK LINE>
Fixes #<issue number>
```

Breaking Change section should start with the phrase "BREAKING CHANGE: " followed by a summary of the breaking change, a blank line, and a detailed description of the breaking change that also includes migration instructions.


### Revert commits

If the commit reverts a previous commit, it should begin with `revert: `, followed by the header of the reverted commit.

The content of the commit message body should contain:

- information about the SHA of the commit being reverted in the following format: `This reverts commit <SHA>`,
- a clear description of the reason for reverting the commit message.

