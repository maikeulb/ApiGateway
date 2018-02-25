module Profile
open Http
open UserProfile
open FSharp.Control.Reactive
open System.Reactive.Threading.Tasks
open ObservableExtensions

let getProfile username =

    let userStream = username |> userUrl |> asyncResponseToObservable

    let toPostWithLanguagesStream (post : UserPosts.Root) =    
        username
        |> languagesUrl post.Name
        |> asyncResponseToObservable
        |> Observable.map (languageResponseToPostWithLanguages post)

    let popularPostsStream = 
        username
        |> postsUrl 
        |> asyncResponseToObservable 
        |> Observable.map postsResponseToPopularPosts
        |> flatmap2 toPostWithLanguagesStream
    
    async {
        return! popularPostsStream
                |> Observable.zip userStream
                |> Observable.map toProfile
                |> TaskObservableExtensions.ToTask 
                |> Async.AwaitTask
    }
