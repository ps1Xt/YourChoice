import React, { useEffect, useState } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import {  Divider, Grid, NativeSelect, Paper, Radio, RadioGroup, TextField } from '@material-ui/core';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from "yup";
import UploadPhoto from './UploadPhoto';
import { createPost } from '../../api/post/CreatePost';
import { UploadPhotos } from './UploadPhotos';
import { PostPartsForCreate } from '../../api/post/Models/PostPartsForCreate';
import { PostForCreate } from '../../api/post/Models/PostForCreate';
const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        PostRedactor: {
            padding: '10px 0',
        },
        container: {
            margin: '10px 20px',
        },
        textDecoration: {
            color: '#525252'
        }
    }),
);



const schema = yup.object().shape({

});


export default function CreatePost() {
    const sizeList: number[] = [8, 16, 32, 64, 128, 256]

    const classes = useStyles();
    let [namePostParts, setNamePostParts] = useState<string[]>([])
    let [title, setTitle] = useState<string>("")
    let [description, setDescription] = useState<string>("")
    let [size, setSize] = useState<number>(8)
    let [postPartNames, setPostPartNames] = useState<string[]>([])
    let [postPartSrc, setPostPartSrc] = useState<any[]>([])
    let [postPartFiles, setPostPartFiles] = useState<File[]>([])
    const form = useForm({
        resolver: yupResolver(schema)
    });
    const { register, handleSubmit, errors } = form;
    let items = []
    for (let i = 1; i <= size; i++) {
        items[i] = <div key={i} style={{ margin: '50px 0' }}>
            <UploadPhoto id={i} postPartNames={postPartNames} setPostPartNames={setPostPartNames}
                postPartSrc={postPartSrc} setPostPartSrc={setPostPartSrc}
                postPartFiles={postPartFiles} setPostPartFiles={setPostPartFiles}
                label="Title"
            />
        </div>
    }
    const onSubmitHandler = (e: any) => {
        console.log(e)
        let postParts = new Array<PostPartsForCreate>()
        for (let i = 0; i <= size; i++) {
            let postPart: PostPartsForCreate = {
                title: postPartNames[i],
                file: postPartFiles[i]
            }
            postParts[i] = postPart
        }
        let post: PostForCreate = {
            description: e.description,
            size: size,
            postParts: postParts
        }
        createPost(post)

    }
    const changeSizeHandler = (e: any) => {
        let size: number = e.target.value
        setSize(size)
    }
    console.log(postPartFiles)
    console.log(postPartNames)
    return (
        <form onSubmit={handleSubmit((e) => onSubmitHandler(e))} autoComplete="off">
            <div style={{ margin: '40px 0px' }}>
                <Paper>
                    <Grid container >
                        <Grid container justify="center">
                            <Grid item>
                                <h1 className={classes.PostRedactor}>POST REDACTOR</h1>
                            </Grid>
                        </Grid>
                        <Grid container spacing={2} className={classes.container}>
                            <Grid item xs={12}>
                                <UploadPhoto id={0} postPartNames={postPartNames} setPostPartNames={setPostPartNames}
                                    postPartSrc={postPartSrc} setPostPartSrc={setPostPartSrc}
                                    postPartFiles={postPartFiles} setPostPartFiles={setPostPartFiles}
                                    label="Enter post Title"
                                />
                            </Grid>
                        </Grid>
                        <Grid container spacing={2} className={classes.container}>
                            <Grid item xs={12}>
                                <Typography className={classes.textDecoration} variant="h5">
                                    Enter post description
                                </Typography>
                            </Grid>
                            <Grid item xs={6}>
                                <TextField
                                    label="Enter post description"
                                    variant="outlined"
                                    color="primary"
                                    fullWidth
                                    name="description"
                                    inputRef={register}
                                />
                            </Grid>
                        </Grid>
                        <Grid container spacing={2} className={classes.container}>
                            <Grid item xs={12}>
                                <Typography className={classes.textDecoration} variant="h5">
                                    Enter post size
                                </Typography>
                            </Grid>
                            <Grid item xs={6}>
                                <NativeSelect
                                    style={{ fontSize: 20 }}
                                    name="size"
                                    inputRef={register}
                                    onChange={changeSizeHandler}
                                >
                                    {sizeList.map((value: Number) => {
                                        return <option >{value}</option>
                                    })}
                                </NativeSelect>
                            </Grid>
                        </Grid>
                    </Grid>
                    <Divider />
                    <UploadPhotos
                        postPartNames={postPartNames}
                        postPartSrc={postPartSrc}
                        postPartFiles={postPartFiles}>
                    </UploadPhotos>
                    <Divider />
                    {items}
                    <div style={{ padding: '100px 0' }}>

                        <Grid container justify="center">
                            <Grid item>
                                <Button type="submit" variant="contained" color="primary">Create Post</Button>
                            </Grid>
                        </Grid>
                    </div>

                </Paper>
            </div>
        </form>
    )
}
