module Profile

open Http
open UserProfile
open FSharp.Control.Reactive
open System.Reactive.Threading.Tasks
open ObservableExtensions

let getProfile username =

    let userStream = username |> userUrl |> asyncResponseToObservable

    let toRepoWithLanguagesStream (repo : UserPosts.Root) =    
        username
        |> languagesUrl repo.Name
        |> asyncResponseToObservable
        |> Observable.map (languageResponseToPostWithLanguages repo)

    let popularPostsStream = 
        username
        |> postsUrl 
        |> asyncResponseToObservable 
        |> Observable.map postsResponseToPopularPosts
        |> flatmap2 toRepoWithLanguagesStream
    
    async {
        return! popularPostsStream
                |> Observable.zip userStream
                |> Observable.map toProfile
                |> TaskObservableExtensions.ToTask 
                |> Async.AwaitTask
    }
