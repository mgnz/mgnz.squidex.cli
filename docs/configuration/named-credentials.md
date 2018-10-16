# Named Credentials

Named Credentials (referenced by `[-c|--named-credentials]`) are dictionary objects stored in the configuraiton file under the `namedCredentials` node.

As many named credentials that are required can be stored; two formats are supported; the first being oauth creds; the second being token creds.

If you supply all elements (clientid, clientsecret and a token e.g. `sample-oauth-client-token`) then only one will be selected.

## Example

```json
{
  ...
  "namedCredentials": {
    "sample-oauth-client-credentials": {
      "baseAddress": "https://a.b.c:80",
      "app": "aut",
      "clientId": "aut:aut-testclient",
      "clientSecret": "V3DJ6r812345678907bx2DI/QvIkyUnQ="
    },
    "sample-token": {
      "baseAddress": "https://a.b.c:80",
      "app": "aut",
      "token": "V3DJ6r812345678907bx2DI/QvIkyUnQ="
    },
    "sample-oauth-client-token": {
      "baseAddress": "https://a.b.c:80",
      "app": "aut",,
      "clientId": "aut:mgnz-aut-testclient",
      "clientSecret": "V3DJ6r812345678907bx2DI/QvIkyUnQ=",
      "token": "V3DJ6r812345678907bx2DI/QvIkyUnQ="
    }
    ...
  }
}
```

**sample-oauth-client-credentials** can be any value; and would be referenced from any [-c|--named-credentials] parameter.