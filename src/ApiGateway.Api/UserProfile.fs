module UserProfile

open Http
open FSharp.Data

type UserProfile = JsonProvider<"user.json">
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
let postsUrl = sprintf "%s/users/%s/repos" host
let languagesUrl postName userName = sprintf "%s/repos/%s/%s/languages" host userName postName

let parseUser = UserProfile.Parse
let parseUserPosts = UserPosts.Parse



let popularPosts (posts : UserPosts.Root []) =
    
    let ownPosts = posts |> Array.filter (fun post -> not post.Fork)
    let takeCount = if ownPosts.Length > 3 then 3 else posts.Length

    ownPosts
    |> Array.sortBy (fun r -> -r.StargazersCount)
    |> Array.toSeq
    |> Seq.take takeCount
    |> Seq.toArray

let parseLanguages languagesJson =
    languagesJson
    |> JsonValue.Parse
    |> JsonExtensions.Properties
    |> Array.map fst
    
let languageResponseToPostWithLanguages (post : UserPosts.Root) = function
    |Ok(l) -> {Name = post.Name; Languages = (parseLanguages l); Stars = post.StargazersCount}
    |_ -> {Name = post.Name; Languages = Array.empty; Stars = post.StargazersCount}

let postsResponseToPopularPosts = function
    |Ok(r) -> r |> parseUserPosts |> popularPosts
    |_ -> [||]

let toProfile  = function
    |Ok(u), posts -> 
        let user = parseUser u
        {Name = user.Name; PopularRepositories = posts; AvatarUrl = user.AvatarUrl} |> Some
    | _ -> None
