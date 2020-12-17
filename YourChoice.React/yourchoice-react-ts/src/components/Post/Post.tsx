import { CircularProgress, Container, createStyles, Grid, OutlinedInput, makeStyles, Paper, Typography, Divider, IconButton, Button, Box } from '@material-ui/core';
import React, { useContext, useEffect, useState } from 'react'
import versus from './versus.png'
import FavoriteIcon from '@material-ui/icons/Favorite';
import AddCircleIcon from '@material-ui/icons/AddCircle';
import { useForm } from 'react-hook-form';
import { comment } from '../../api/comment/Comment';
import { Comment } from './Comment'
import { addToFavorites } from '../../api/favorites/AddToFavorites';
import { subscribe } from '../../api/subscription/Subscribe'
import Rating from '@material-ui/lab/Rating/Rating';
import { ratePost } from '../../api/rating/RatePost';
import { PostPartForView } from '../../api/post/Models/PostPartForView';
import { PostForView } from '../../api/post/Models/PostForView';
import { PostComment } from '../../api/post/Models/PostComment';
import { GetPost } from '../../api/post/GetPost';
import { getCommentByUrl } from '../../api/comment/GetCommentByUrl';
import removeFromFavorites from '../../api/favorites/RemoveFromFavorites';
import { unSubscribe } from '../../api/subscription/UnSubscribe';
import { ErrorBox } from '../Common/ErrorBox';
import { ErrorObject } from '../../models/ErrorObject'
import { SignalRContext } from '../../Context/SignalRContext'

const useStyles = makeStyles(() =>
    createStyles({
        photoContainer: {
            display: 'flex',
            justifyContent: 'flex-end',
            alignItems: "center",
            height: '100%',
            position: 'relative'

        },
        image: {

        }
    }),
);
const getBoxHeight = () => {
    return (getHeight() - 64 < 700 ?
        getHeight() - 64 :
        700);
}
const getWidth = () => {
    return document.documentElement.clientWidth;
}
const getHeight = () => {
    return document.documentElement.clientHeight;
}
const shuffle = (arr: PostPartForView[]) => {
    let j, x, i;
    for (i = arr.length - 1; i > 0; i--) {
        j = Math.floor(Math.random() * (i + 1));
        x = arr[i];
        arr[i] = arr[j];
        arr[j] = x;
    }
    return arr;
}

let defaultPost: PostForView = {
    id: 1,
    title: "",
    userName: "",
    userId: 1,
    description: "",
    postParts: new Array<PostPartForView>(),
    date: "",
    comments: new Array<PostComment>(),
    isInFavorites: false,
    isSubscribed: false,
    avgRating: 0,
    size: 8,
}
export const Post = (props: any) => {
    const context = useContext(SignalRContext)

    const { register, handleSubmit, errors } = useForm();
    const classes = useStyles();
    const [height, setHeight] = useState(getBoxHeight())
    const { match: { params } } = props;
    const [commentValue, setCommentValue] = useState("");


    let [isLoading, setIsLoading] = useState<boolean>(true)
    let [error, setError] = useState<ErrorObject>({ status: false })
    let [post, setPost] = useState<PostForView>(defaultPost)
    let [leftPic, setLeftPic] = useState<string>();
    let [rightPic, setRightPic] = useState<string>();
    let [leftName, setLeftName] = useState<string>();
    let [rightName, setRightName] = useState<string>();
    let [winner, setWinner] = useState<PostPartForView>({})
    let [prevPostParts, setPrevPostParts] = useState<PostPartForView[]>(new Array<PostPartForView>())
    let [nextPostParts, setNextPostParts] = useState<PostPartForView[]>(new Array<PostPartForView>())
    let [comments, setComments] = useState<PostComment[]>(new Array<PostComment>())
    let [size, setSize] = useState<number>(4);
    let [step, setStep] = useState<number>(1);
    const [defaultAvgRating, setDefaultAvgRating] = useState(0)
    window.onresize = () => setHeight(getBoxHeight())

    const LoadPost = async () => {
        try {
            let result = await GetPost(params.postId)
            setPost(result);
            if (result.postParts != undefined) {
                setLeftPic(result.postParts[0].link)
                setRightPic(result.postParts[1].link)
                setLeftName(result.postParts[0].title)
                setRightName(result.postParts[1].title)
                setPrevPostParts(result.postParts);
                setSize(result.size);
                comments = result.comments?.reverse() ?? [];
                setComments(comments);
                setDefaultAvgRating(result.avgRating)
            }

        }
        catch (ex) {
            setError({ status: true, message: ex.message })
        }
        finally {
            setIsLoading(false);

        }



    }

    useEffect(() => {
        LoadPost()


    }, [])

    const clickHandler = (coef: number) => {
        if (step < prevPostParts.length / 2) {
            nextPostParts.push(prevPostParts[(step - 1) * 2 + coef])
            setNextPostParts(nextPostParts)
            step++;
            setStep(step)
            setLeftPic(prevPostParts[(step - 1) * 2].link)
            setRightPic(prevPostParts[(step - 1) * 2 + 1].link)
            setLeftName(prevPostParts[(step - 1) * 2].title)
            setRightName(prevPostParts[(step - 1) * 2 + 1].title)
        }
        else if (prevPostParts.length != 2) {
            nextPostParts.push(prevPostParts[(step - 1) * 2 + coef])
            setNextPostParts(nextPostParts)
            prevPostParts = nextPostParts
            setPrevPostParts(nextPostParts)
            setSize(size / 2)
            setNextPostParts(new Array<PostPartForView>())
            setLeftPic(prevPostParts[0].link)
            setRightPic(prevPostParts[1].link)
            setLeftName(prevPostParts[0].title)
            setRightName(prevPostParts[1].title)
            setStep(1)
        }
        else if (winner.link == undefined) {
            setWinner(prevPostParts[(step - 1) * 2 + coef])
            return;

        }
    }
    const LeaveCommentHandler = async (e: any) => {
        try {
            if (e.comment == "")
                return
            let url = await comment(post.id, e.comment);
            let data = await getCommentByUrl(url);
            comments.unshift(data);
            setComments(comments)
            context.CommentNotify(post.id);
        }
        catch (ex) {
            console.log(ex.message)
        }


    }
    const favoritesHandler = () => {
        try{
            if (post.isInFavorites) {
                removeFromFavorites(post.id);
            }
            else {
                addToFavorites(post.id);
                context.FavoritesNotify(post.id)
    
            }
        }
        catch{

        }

        setPost({ ...post, isInFavorites: !post.isInFavorites })
    }
    const rateHandler = async (value: number) => {
        if (value != null) {
            try {
                let result = await ratePost(post.id, value);
                context.RatingNotify(post.id)
                setPost({ ...post, avgRating: result.avgRating })

            }
            catch (ex) {
                console.log(ex.message)
            }
            
        }

    }
    const onCommentChange = (str: string) => {
        setCommentValue(str);
    }
    const onCLick = () => {
        setTimeout(() => {
            setCommentValue("");
        }, 100)
    }
    const subscriptionHandler = async () => {
        if (post.isSubscribed) {
            try {

                await unSubscribe(post.userName);
            }
            catch {
            }
        }
        else {
            try {
                await subscribe(post.userName);
                context.SubscriptionNotify(post.id)

            }
            catch {
            }
        }
        setPost({ ...post, isSubscribed: !post.isSubscribed })
    }

    if (isLoading)
        return (<div style={{ height: `${getHeight() - 64}px`, width: `100%`, display: 'flex', justifyContent: 'center', alignItems: 'center' }}><CircularProgress /></div>)
    if (error.status) {
        return <ErrorBox message={error.message} />
    }
    else
        return (
            <div style={{ width: '100%', background: '#464646', height: `${height}px`, maxHeight: '700px', position: "absolute", top: '0px', left: '0px' }}>
                { winner.link == undefined && <img src={versus} style={{ width: '150px', zIndex: 10, position: 'absolute', left: '50%', marginLeft: '-75px', marginTop: '-67px', top: '50%' }} />}
                <Grid container  >
                    <Grid item xs={12}>
                        <div style={{ position: 'absolute', zIndex: 10, overflow: 'hidden', height: '50px', width: '100%', backgroundColor: 'rgba(0, 0, 0, 0.4)', textAlign: 'center', fontSize: '42px', color: 'white', fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif' }}>
                            {post.title} {step}/{size / 2}
                        </div>
                    </Grid>
                    <Grid item xs={12}>
                        <div style={{ position: 'absolute', zIndex: 10, top: '50px', overflow: 'hidden', fontSize: '50px', color: 'white', fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif' }}>
                            <IconButton onClick={favoritesHandler} >
                                <FavoriteIcon fontSize='large' color={post.isInFavorites ? "secondary" : "primary"} />
                            </IconButton >

                        </div>
                    </Grid>
                    <Grid item xs={12}>
                        <div style={{ position: 'absolute', zIndex: 10, top: '100px', overflow: 'hidden', fontSize: '50px', color: 'white', fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif' }}>
                            <IconButton onClick={subscriptionHandler}>
                                <AddCircleIcon fontSize='large' color={post.isSubscribed ? "secondary" : "primary"} />
                            </IconButton >

                        </div>
                    </Grid>

                    {winner.link == undefined && <Grid item xs={6}>
                        <div className={classes.photoContainer} style={{ minHeight: `${getBoxHeight()}px` }}>


                            <img style={{ maxWidth: '95%', maxHeight: `${height}px`, cursor: 'pointer' }} onClick={() => clickHandler(0)} src={leftPic} />
                            <div style={{ position: 'absolute', overflow: 'hidden', height: '50px', width: '100%', bottom: '0px', backgroundColor: 'rgba(0, 0, 0, 0.4)', textAlign: 'center', fontSize: '42px', color: 'white', fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif' }}>
                                {leftName}
                            </div>
                        </div>


                    </Grid>}
                    {winner.link == undefined && <Grid item xs={6}>
                        <div className={classes.photoContainer} style={{ justifyContent: 'flex-start', minHeight: `${getBoxHeight()}px` }}>

                            <img style={{ maxWidth: '95%', maxHeight: `${height}px`, cursor: 'pointer' }} onClick={() => clickHandler(1)} src={rightPic} />
                            <div style={{ position: 'absolute', overflow: 'hidden', height: '50px', width: '100%', bottom: '0px', backgroundColor: 'rgba(0, 0, 0, 0.4)', textAlign: 'center', fontSize: '42px', color: 'white', fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif' }}>
                                {rightName}
                            </div>
                        </div>
                    </Grid>}
                    {winner.link != undefined &&
                        <Grid item xs={12}>
                            <div className={classes.photoContainer} style={{ justifyContent: 'center', minHeight: `${getBoxHeight()}px` }}>

                                <img style={{ maxWidth: '95%', zIndex: 1, maxHeight: `${height}px`, cursor: 'pointer' }} onClick={() => clickHandler(1)} src={winner.link} />
                                <div style={{ position: 'absolute', zIndex: 10, overflow: 'hidden', height: '50px', width: '100%', bottom: '0px', backgroundColor: 'rgba(0, 0, 0, 0.4)', textAlign: 'center', fontSize: '42px', color: 'white', fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif' }}>
                                    {winner.title} won
                                </div>
                            </div>
                        </Grid>
                    }

                </Grid>
                <Paper >
                    <Container >
                        <Grid container justify='center'>
                            <Grid item>
                                <Box component="fieldset" mb={3} borderColor="transparent">
                                    <Rating name="customized-10" defaultValue={defaultAvgRating} max={10} onChange={(e: any, value: any) => rateHandler(value)} />
                                </Box>
                            </Grid>
                        </Grid>

                        <div style={{ padding: '20px 0px 60px 0px' }}>
                            <Divider />
                            <Grid container>

                                <Grid item xs={12} style={{ marginTop: '20px' }}>
                                    <Typography variant="h5">
                                        Author : <span style={{ fontWeight: 'bold' }}>{post.userName}</span>
                                    </Typography>


                                </Grid>
                                <Grid item xs={12}>
                                    <Typography variant="h5">
                                        Rating: <span style={{ fontWeight: 'bold' }}>{post.avgRating}</span>
                                    </Typography>
                                </Grid>
                                <Grid item xs={12}>
                                    <Typography variant="h5">
                                        Publication date: <span style={{ fontWeight: 'bold' }}>{post.date}</span>
                                    </Typography>
                                </Grid>
                                <Grid>
                                    <Typography variant="h5">
                                        Description:
                                </Typography>
                                </Grid>
                                <Grid item xs={12}>
                                    <Typography variant="h5" >
                                        <span style={{ fontStyle: 'italic', fontWeight: 'bold' }}>
                                            {post.description}
                                        </span>

                                    </Typography>

                                </Grid>
                            </Grid>
                            <Divider />
                        </div>
                    </Container>



                    <Container style={{ marginTop: '20px' }}>
                        <form onSubmit={handleSubmit(LeaveCommentHandler)}>
                            <Grid container justify='flex-start' spacing={1}>
                                <Grid item xs={12}>
                                    <OutlinedInput
                                        id="outlined-multiline-static"
                                        multiline
                                        rows={2}
                                        rowsMax={100}
                                        placeholder="Leave a comment"
                                        fullWidth
                                        name="comment"
                                        inputRef={register}
                                        value={commentValue}
                                        onChange={(e) => onCommentChange(e.target.value)}
                                    />

                                </Grid>
                                <Grid item xs={12}>
                                    <div style={{ display: 'flex', justifyContent: "flex-end", }}>
                                        <Button
                                            variant="contained"
                                            color="primary"
                                            type="submit"
                                            onClick={onCLick}
                                        >
                                            COMMENT
                                        </Button>
                                    </div>
                                </Grid>
                            </Grid>
                        </form>
                    </Container>
                    <Container style={{ marginTop: '20px' }}>
                        <Divider />
                        <Grid container spacing={5} direction="column">
                            {comments.map((comment) => {
                                {
                                    return (
                                        <Grid item key={comment.id}>
                                            <Comment userName={comment.userName} date={comment.date} text={comment.text} />
                                        </Grid>)
                                }

                            })}
                        </Grid>
                    </Container>
                </Paper>
            </div>
        )
}