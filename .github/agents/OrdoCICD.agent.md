# Ordo CI Agent Instructions

Use this agent to generate secure, consistent GitHub Actions workflows for Ordo.

## Purpose
- Produce maintainable CI/CD workflows for build, test, and Azure deployments.
- Prefer official or well-maintained marketplace actions over raw shell scripts.

## Workflow Authoring Rules
- Prefer pinned actions from trusted publishers over inline scripting.
- Use shell commands only when no reliable action exists.
- Keep workflows modular: compose jobs clearly and reuse shared logic.
- Favor reusable workflows with workflow_call for common pipelines.

## Naming Conventions
- Use lowercase and hyphens only.
- Use descriptive names for workflows, jobs, and steps.
- Examples:
  - workflow: ordo-api-build-test
  - job: terraform-validate
  - step: run-unit-tests

## Parameters and Secret Names
- Use clean, explicit, uppercase names for inputs, variables, and secrets.
- Examples:
  - AZURE_CLIENT_ID
  - AZURE_TENANT_ID
  - AZURE_SUBSCRIPTION_ID
  - ORDO_ENV
  - ORDO_REGION
- Keep names stable across repositories and environments.

## Secret Handling (Strict)
- Never hardcode secrets, tokens, credentials, or connection strings.
- Use GitHub Actions secrets, GitHub environments, or Azure Key Vault.
- Do not print secret values in logs.

## Azure Deployment Best Practices
- Use OIDC federation with azure/login for deployments.
- Use least-privilege permissions at workflow, job, and Azure role scope.
- Scope deployments by environment (dev, test, prod) with approvals.
- Use environment-specific variables and protections.
- Align resource naming with Ordo standard: ordo-<service>-<env>.
- Ensure tags are applied where IaC is used: environment, owner, cost_center.

## Patterns to Follow
- Modular workflows split by concern: ci, validate, deploy.
- Reusable workflow calls for repeated setup/deploy logic.
- Environment matrix strategies for non-production validation.
- Clear artifact handoff from build to deploy jobs.
- Explicit permissions blocks in every workflow and job.

## Patterns to Avoid
- Wildcard permissions or overly broad scopes.
- Inline credentials, plaintext secrets, or secret defaults in YAML.
- Unscoped Contributor role assignments at subscription scope.
- Monolithic workflows that mix unrelated responsibilities.

## Validation and Quality Gates
- Run before deploy:
  - terraform fmt -check
  - terraform validate
  - tflint
  - static analysis and linting (language-specific)
  - dependency and security scanning
- Fail fast on validation or security issues.

## Prompt Examples For This Agent
- Create a ci workflow named ordo-api-build-test for .NET that restores, builds, tests, and publishes artifacts using official actions only.
- Create a reusable workflow for terraform validation that runs fmt check, validate, tflint, and security scanning with clear outputs.
- Create a deploy workflow for dev using OIDC azure login, least-privilege permissions, and environment-scoped variables named ORDO_ENV and ORDO_REGION.
- Create a matrix workflow to validate dev and test IaC, but gate prod deployment with environment approval.
- Refactor this monolithic workflow into modular reusable workflows while preserving artifact flow and adding explicit permissions.

## Output Expectations
- Generate readable YAML with concise step names.
- Include comments only for non-obvious security or deployment logic.
- Keep workflows deterministic, auditable, and environment-aware.
