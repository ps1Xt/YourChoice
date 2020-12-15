import { Container, createStyles, Grid, makeStyles, TextField, Theme, Typography } from '@material-ui/core'
import React, { useEffect, useState } from 'react'
import DragAndDrop from './DragNDrop'
import DefaultPhoto from './default.png'
import { useSelector } from 'react-redux';
import CombinedStore from '../../store/CombinedStore';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        photoBox: {
            width: '150px',
            height: '150px',
            borderRadius: '10px',
            overflow: 'hidden',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            background: 'white',
            backgroundSize: '150px ',
            backgroundRepeat: 'no-repeat'


        },
        photoSrc: {
            maxWidth: '150px',
            height: 'auto',
            maxHeight: "150px",
            borderRadius: '7px'
        },

    }),
);
export const UploadPhoto = (props: any) => {
    const classes = useStyles();
    let { id,
        postPartNames,
        setPostPartNames,
        postPartSrc,
        setPostPartSrc,
        postPartFiles,
        setPostPartFiles,
        label,
        text
    } = props;


    let [photoSrc, setPhotoSrc] = useState<any>(DefaultPhoto);
    let [partName, setPartName] = useState<string>("");

    let update = useSelector<CombinedStore, number>((s) =>
        s.createPost.update
    )
    useEffect(() => {
        setPartName(postPartNames[id])

    }, [postPartNames[id]])

    useEffect(() => {
        setPhotoSrc(postPartSrc[id] ? postPartSrc[id] : DefaultPhoto)

    }, [postPartNames[id]])

    const onTitleChange = (value: any) => {
        postPartNames[id] = value
        setPostPartNames(postPartNames)
        setPartName(value)
    }
    const fileHandler = (_files: File[]) => {
        let file = _files[0];
        let name = file.name.split('.')[0];

        postPartFiles[id] = file;
        setPostPartFiles(postPartFiles)

        postPartNames[id] = name;
        setPostPartNames(postPartNames)

        setPartName(name)


        let reader = new FileReader();
        reader.readAsDataURL(file)
        reader.onload = () => {
            postPartSrc[id] = reader.result
            setPostPartSrc(postPartSrc)
            setPhotoSrc(reader.result)
        }

    }
    return (
        <div>
            <Container>
                <Grid container alignItems="center" spacing={3}>
                    <Grid item>
                        <Typography variant="h6">Title:</Typography>
                    </Grid>
                    <Grid item xs={11}>
                        <TextField
                            id="outlined-basic"
                            fullWidth
                            label={label}
                            variant="outlined"
                            value={partName}
                            onChange={e => onTitleChange(e.target.value)}
                        />
                    </Grid>
                </Grid>

            </Container>

            <Container style={{ marginTop: "20px" }}>
                <Grid container spacing={10} alignItems="center">
                    <Grid item>
                        <div className={classes.photoBox} style={{ backgroundImage: `url("${photoSrc}")` }}>
                        </div>
                    </Grid>
                    <Grid item xs={9}>
                        <DragAndDrop filesHandler={fileHandler} text={text}/>
                    </Grid>
                </Grid>
            </Container>
        </div>

    )
}
export default UploadPhoto