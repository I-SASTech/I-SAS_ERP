<template>
    <div >
        <div class="picture" @click="$refs.imgupload.click()">
            <img v-if="filePath != null && filePath != undefined"
                 :src="'data:image/png;base64,' + filePath"
                 style="width: 50px;" />

            <img v-else v-bind:src="image"  style="width: 50px;"  />

        </div>
       
    </div>
</template>
<script>
    //import axios from 'axios';
    export default {
        props: ["path"],
        data() {
            return {
                image: '/images/Product.png',
                renderedImage: 0,
                data: [], 
                url: '',
                filePath: null
            }
        },
        methods: {
          
              getBase64Image: function (path) {                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        
                        if (response.data != null) {
                            root.filePath = response.data;
                        }
                    });
            },
        },
        mounted: function () {
            
            
            if (this.path == "") {
               /* this.filePath = this.image;*/
            }
            else {
                this.getBase64Image(this.path);
            }
        }
    }
</script>

