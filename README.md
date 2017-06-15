# Migration to GitHub

## Rationale
The rationale for migration to a better infrastructure comes from the need to streamline development and establish proper controls of code quality being committed to repositories as well as automating code validation, testing and deployment routines.

While the team involved in WMF application delivery is growing, it is important that we focus on introducing streamlined process, tooling and quality tollgates to ensure only proper contributions end up in our repositories.

GitHub comes naturally as first choice since it's gaining popularity with UBS WMA and provides all functionality that will allow us achieving our goals:

- Benefits of git's local/remove repositories
- Code review facility
- Ability to manage pushes via *pull requests* to ensure no toxic contributions are being made to master/release branches
- WebHooks and Services Integration for integration with CI and other external systems (code scanning, tests execution and more)
- Wiki/Pages for developer's documentation and manuals

## Ideal Workflow

1. No pushed to master until the branch has been fully tested and validated
2. All Releases (Sprints) work is done in branches
3. All features (Story) are developed in branches and merged into Release branch when ready and tested
4. All merges are done via pull requests
5. Pull Request initiate code scans and checks (changes that break build/fail tests or aren't convention compliant are rejected automatically)
6. Ideally, since some of tests will require application to be deployed for testing, the CI/CD would need to be able to deploy the packages to DEV/SIT without having them pre-committed to Artifactory or Nexus
7. Once the application passes DEV/SIT testing, the pull requests are merged into master
8. We will need to work together to decide which branch is used to build the QA/Production bundles

## Scenarios
While there are multiple issues to be resolved in WMA at IT4IT level to unlock full infrastructure, there are two scenarios that could enable basic setup with GitHub:

### One: Leveraging existing components as much as possible

- GitHub
- Jenkins
- Nexus Pro

#### Pros:
- Uses, by most, existing infrastructure,
- Most of existing population have experience with these systems;

#### Cons:
- Jenkins does not have access to GitHub (SVN server only at this point),
- Developers cannot create Jenkins jobs themselves and have to request assistance from support team - this is not going to work  well with GitHub vanilla workflow where CI will need to build changes in particular branches vs master.

### Two: Replace Jenkins with something developers can administer themselves + Artifactory

- GitHub
- TeamCity (already adopted by UBS)
- Artifactory

#### Pros:
- Developers are able to configure CI server to their needs without support proxy and time lost;

#### Cons:
- TeamCity instances will not have access to Artifactory (only Jenkins do at this point) - there is no reliable solution other than pushing changes to svn trunk to be picked by Jenkins - this may address particular CI interests of individual teams (albeit the svn loopback hassle for QA builds) but does not address the problem entirely


## Dependencies
- Availability of production git (or GitHub environment)
- Access to git from Jenkins (current doesn't have it)
- OR Access from TeamCity to Artifactory (currently, only Jenkins has access to Artifactory)
- Development population are hands-on with GIT and understand its main workflows

## Pre-GIT Migration Work

When all dependencies are met, the following work could be commenced to ensure smooth migration:

1. Have all committers push their changes into svn repository
2. Have all committers install git client and validate it works with production git server (with a test repository)


## GIT Migration

We are suggesting use of git-svn to channel the changes from svn to git; this is a simple process:

1. Build list of committers and convert it to git format manually
2. Clone the entire svn repository with git-svn
3. Convert property files from svn to git format
4. Create a bare git repository and push cloned svn repository into it
5. Rename trunk to master
6. Convert svn tags into git tags
7. Validate migration completed successfully, check all users have access to the new repository
8. Have all committers clone the new repository from git

## More than GIT

GitHub comes with a load of great tools that help developers automate and streamline their development routines and it does make sense to use them to full extend.

### Github Workflows

Suggested GitHub workflow encourages use of pull requests and code review practices before changes are merged into master. This allows keeping master clean and making sure no toxic changes pass uncaught.  
    
Development team will need to master proper branching and merging policy to keep the whole group synchronized:

- One Standard for branching policy
- Understanding of code standards and quality tollgates (convention, violations, unit test coverage)
- Pull requests and code review policy

### CI Integration

CI systems are important part of this workflow - these allow automation of various code checks, builds and test execution as well as feeding back to GitHub to help it determine the logic of the workflow.

This is typically done via WebHook or Services Integration. 

### Implementation

The following steps are suggested for the implementation:

1. Adjustments of the dev process based on available infrastructure and project specifics
2. Team trainings and knowledge sharing
3. Setup and configuration of GitHub
4. Migration of repository from SVN
5. Pilot for a select group of users (with git to svn reverse flow, manual)
6. Expansion to the whole project population on successful pilot





