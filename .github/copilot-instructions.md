# GitHub Copilot Instructions for Ordo

Use GitHub Copilot to speed up delivery, but keep Azure quality, security, and consistency as non-negotiable standards.

## 1) Azure Naming Convention (Required)

Name Azure resources with this pattern:

`ordo-<service>-<env>`

Rules:
- Use lowercase only
- Use hyphens only (no underscores/spaces)
- Keep names consistent across Bicep, scripts, and workflows
- Examples:
  - `ordo-rg-dev`
  - `ordo-appsvc-test`
  - `ordo-keyvault-prod`

## 2) Required Azure Tags (Required)

Every resource must include these tags:
- `environment`
- `owner`
- `cost_center`

Guidance:
- `environment`: `dev`, `test`, or `prod`
- `owner`: team or person accountable for the resource
- `cost_center`: billing/accounting identifier used by FinOps

Do not accept Copilot output that creates untagged resources.

## 3) Patterns to Follow

Copilot output should align to these repository standards:
- Modular Bicep:
  - Use reusable modules for resource groups, networking, compute, and monitoring
  - Keep parameters explicit and environment-specific
- Secure defaults:
  - Private endpoints or network restrictions where applicable
  - HTTPS/TLS enforced
  - Diagnostic settings and logging enabled
- Least privilege IAM:
  - Assign only required roles
  - Scope role assignments as narrowly as possible
  - Prefer managed identities over secrets

## 4) Patterns to Avoid

Reject or rewrite Copilot output that includes:
- Wildcard permissions (`*`, overly broad `Contributor` at high scope)
- Hardcoded secrets, keys, connection strings, or tokens
- Inconsistent naming that does not follow `ordo-<service>-<env>`
- Resources without required tags (`environment`, `owner`, `cost_center`)

## 5) Writing Effective Prompts for Copilot

When prompting Copilot, always include:
- Environment (`dev|test|prod`)
- Region
- Resource names in Ordo format
- Security constraints
- Tag requirements
- Expected output format (Bicep, Bash/PowerShell, YAML)

### IaC Prompt Template
“Create modular Bicep for `<service>` in `<env>` using name `ordo-<service>-<env>`. Include tags `environment`, `owner`, `cost_center`; secure defaults; no hardcoded secrets; and least-privilege role assignments.”

### Script Prompt Template
“Generate `<bash|powershell>` script to deploy/update `<service>` for `<env>`, enforce tags (`environment`, `owner`, `cost_center`), validate naming pattern `ordo-<service>-<env>`, and fail on missing required inputs.”

### GitHub Actions Prompt Template
“Create a GitHub Actions workflow for Azure deployment of `<service>` in `<env>` using OIDC (no stored secrets), environment approvals, lint/validate steps for Bicep, and a pre-deploy tag/naming compliance check.”

## 6) Verify Copilot Output Before Azure Deployment

Before running deployment commands:
- Validate naming matches `ordo-<service>-<env>`
- Confirm all resources have required tags
- Review IAM scopes for least privilege
- Check for hardcoded secrets or plaintext credentials
- Run Bicep lint/validation and preview changes
- Require peer review for production changes

Copilot accelerates authoring. Engineers remain responsible for security, compliance, and operational correctness.
