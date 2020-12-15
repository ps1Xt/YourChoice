import { Grid } from '@material-ui/core'
import React from 'react'
import { useDispatch } from 'react-redux'
import { setUpdate } from '../../store/actions/_createPost'
import DragAndDrop from './DragNDrop'
export const UploadPhotos = (props: any) => {

    let {
        postPartNames,
        postPartSrc,
        postPartFiles,
        text
    } = props;
    let dispatch = useDispatch()
    const filesHandler = (_files: File[]) => {
        let number = 0;
        for (let i = 1; i <= _files.length; i++) {
            let file = _files[i-1]

            if (file?.type.startsWith("image/")) {
                let name = file.name.split('.')[0]
                postPartNames[i] = name;
                postPartFiles[i] = file;
                let reader = new FileReader();

                reader.readAsDataURL(file)
                reader.onload = () => {
                    postPartSrc[i] = reader.result


                    if (++number == _files.length) {
                        dispatch(setUpdate())
                    }

                }
            }
        }
    }

    return (
        <div style={{margin:'20px 0px'}}>
            <Grid container justify="center">
                <Grid item xs={10}>

                    <DragAndDrop filesHandler={filesHandler} text={text}></DragAndDrop>
                </Grid>
            </Grid>
        </div>
    )
}