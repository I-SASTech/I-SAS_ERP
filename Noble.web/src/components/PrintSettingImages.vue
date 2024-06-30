<template>
    <div class="borderColor" style="position:relative">
        <div @click="$refs.imgupload.click()" class="ImageCenter">
            <img v-if="filePath != null && filePath != undefined" 
                 :src="'data:image/png;base64,' + filePath" />

            <img v-else v-bind:src="image"    class=" rounded mx-auto mt-2  " />

            <div v-if="isImage==false" style="padding-top:10px;font-weight:bold">
                <div v-if="!disable">
                    {{ $t('AddProductImage.UploadProductImage') }}
                </div>
            </div>
        </div>
        <div class="col-sm-12 mt-3" v-if="isDelete && !disable">
            <span style="position:absolute; right:-3px; top:-12px;">
                <button class="btn btn-danger btn-icon-circle btn-icon-circle-sm " v-on:click="DeleteAttachment(imagePath)">
                    <i  class="las la-trash-alt"></i>
                </button>
            </span>
        </div>
        <div class="col-sm-12  " v-if="disable">
            <input ref="imgupload" hidden disabled type="file" accept="image/*" @change="uploadImage('onClick')" id="file-input">
        </div>
        <div class="col-sm-12 " v-else>
            <input ref="imgupload" hidden type="file" accept="image/*" @change="uploadImage('onClick')" id="file-input">
        </div>
    </div>
</template>
<script>
    //import axios from 'axios';
    export default {
        props: ["path", "disable","imagePath"],
        data() {
            return {
                image: '../../ProductSvg.svg',
                data: [], 
                url: '',
                isImage: false,
                isDelete: false,
                filePath:null
            }
        },
        methods: {
            DeleteAttachment(path) {
                

                var token = '';
                var root = this;
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Contact/DeleteAttachment?path=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data == true) {

                        root.isDelete = false;
                        root.isImage = false;
                        root.filePath = null;
                        root.image = '../../ProductSvg.svg';
                        root.$emit('picPath', null);
                    }
                });
            },
            uploadImage() {
                var root = this;
                var file = this.$refs.imgupload.files;
                this.filePath = null;
                var fileData = new FormData();
                this.image = URL.createObjectURL(this.$refs.imgupload.files[0]);
                // convert file into FormData object
                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                }

             //   this.image = fileData;
                
                this.$https.post('/Company/UploadFilesAsync',  fileData)
               .then(function (response) {
                   if (response.data != null) {
                       root.isImage = true;
                       root.isDelete = true;
                       
                        root.$emit('picPath', response.data);
                        //root.image = response.data;
                        }
                    });

                // const URL = 'http://foobar.com/upload';
                //  let data = new FormData();
                //  data.append('name', file.name);
                //  data.append('file', event.target.files[0]);
                //  this.data = data;
            }
        },
        mounted: function () {
            
            this.filePath = this.path == "" ? null : this.path;
            if (this.filePath != null &&
                this.filePath != undefined
            ) {
                this.isImage = true;
                this.isDelete = true;

                this.image = this.filePath;
            }
        }
    }
</script>
<style scoped>
    .borderColor {
        border-style: dashed;
        border: 1px dashed rgba(14, 109, 254, 0.4);
        background-color: #EAF1FE;
        text-align: center;
        height: 150px;
        box-sizing: border-box;
        border-radius: 10px;
    }
    img {
        width: 100%;
        height: 110px;
    }
    .ImageCenter{
    }
</style>
