module GitHub

open Http
open FSharp.Data

type User = JsonProvider<"user.json">
type UserPosts = JsonProvider<"posts.json">

type Profile = {
    Name : string
    AvatarUrl : string
    PopularRepositories : Repository seq
} and Repository = {
    Name : string
    Stars : int
    Languages : string[]
}

let host = "https://api.github.com"
let userUrl = sprintf "%s/users/%s" host
let reposUrl = sprintf "%s/users/%s/repos" host
let languagesUrl repoName userName = sprintf "%s/repos/%s/%s/languages" host userName repoName

let parseUser = GitHubUser.Parse
let parseUserRepos = GitHubUserRepos.Parse
