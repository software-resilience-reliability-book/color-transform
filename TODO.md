## To Add

Complete transforms

Add enum somewhere? Helps with the invariants section of the book.

May be best to cutoff and clone at this point as the "testing reference project".

Not sure if a full solution is really a good idea anyway; better to introduce small concepts

Use this as the "base project" to extend from for the other concepts below. Remove "full" from repo name. Other projects will append their specific concepts to the end of their repo names.

## Needed Topics (in order of likeliness to make scope)

Configuration settings

- We do need practical example of this but not sure how it fits in project

Adding third party dependencies

- May not have time for practical example.

Custom exception types

- Doesn't really buy us anything here, so maybe don't
  include it. This doesn't have to be an uber-project with everything in it.

Dev vs production environments

- May not have time for practical example.

Configuring build output

- May not have time for practical example.

## Consumer Project

```
├── src/
│ ├── ColorTransform/
│ └── ColorTransform.App/
```

ColorTransform.App = very light basic Blazor app that consumes the library and is there only to illustrate integration testing; shows that the library is meant to be consumed by other projects.
