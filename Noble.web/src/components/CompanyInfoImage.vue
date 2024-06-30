<template>
    <div style="border-style: dashed; border-width: 3px;border-color: lightgray;background-color:#fbfbfb; height:364px"   >
        <div  @click="$refs.imgupload.click()" style=" width: auto; height: 200px; padding-top:44px">
            <img v-if="filePath != null && filePath != undefined"
                 :src="'data:image/png;base64,' + filePath"
                 style="height: 150px; width: 150px"
                 />

            <img v-else v-bind:src="image" />
            <div v-if="isImage==false" style="padding-top:20px;font-weight:bold"> {{ $t('CompanyInfoImage.UploadComapnyLogo') }}</div>9090890
        </div>
       
        <div class="col-sm-12 mt-4">
            <input ref="imgupload" hidden type="file" accept="image/*" @change="uploadImage('onClick')" id="file-input">
        </div>
       
    </div>
</template>
<script>
    //import axios from 'axios';
    export default {
        props: ["path"],
        data() {
            return {
                image: '../../assets/img/cloud-computing.png',
                data: [],
                isImage:false,
                url: '',
                filePath: null
            }
        },
        methods: {
           

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

                this.$https.post('/Company/UploadFilesAsync', fileData)
                    .then(function (response) {
                        if (response.data != null) {
                            root.isImage = true;
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
            if (this.path != "" && this.path != undefined) {
                this.isImage = true;

            }
            if (this.filePath != null &&
                this.filePath != undefined
            ) {
                this.image = this.filePath;

            }
        }
    }
</script>
<style scoped>
    img {
       height:150px;
       width:150px
    }
</style>
