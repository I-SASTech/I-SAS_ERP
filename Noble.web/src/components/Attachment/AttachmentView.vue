<template>
<modal :modalLarge="true"  :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-12 text-center" v-if="isImage">
                                    <img :src="filePath" alt="Attachment Image" class="img-fluid" />
                                </div>

                                <div class="col-lg-12 text-center" v-if="isPdf">
                                    <iframe   :src="filePath+'#toolbar=0'" style="width:74%; height:1000px;" class="img-fluid"/>
                                </div>
          
                </div>
            </div>
            <div class="modal-footer">
                  <button class="btn btn-soft-secondary mr-2"
                                            v-on:click="close">
                                        {{ $t('ImportAttachment.Cancel') }}
                                    </button>
            </div> 
        </div>
    </modal>   
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        props: ['show', 'documentpath'],
        mixins: [clickMixin],
        data: function () {
            return {
                arabic: '',
                english: '',
                filePath: '',
                render: 0,
                loading: false,
                isImage: false,
                isPdf: false
            }
        },
        methods: {            
            close: function () {
                this.$emit('close');
            },

            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            if (root.isImage) {
                                root.filePath = 'data:image/png;base64,' + response.data; 
                            }

                            if (root.isPdf) {
                                root.filePath = 'data:application/pdf;base64,' + response.data; 
                            }                                                         
                        }
                    });
            },
        },

        created: function () {
            
            var fileExtension = this.documentpath.split('.').pop();

            if (fileExtension == 'png' || fileExtension == 'PNG' || fileExtension == 'jpg' || fileExtension == 'JPG' || fileExtension == 'jpeg' || fileExtension == 'JPEG' || fileExtension == 'svg' || fileExtension == 'SVG' || fileExtension == 'gif' || fileExtension == 'GIF') {
                this.getBase64Image(this.documentpath);     
                this.isImage = true;
            }

            if (fileExtension == 'pdf' || fileExtension == 'PDF') {
                this.getBase64Image(this.documentpath);
                this.isPdf = true;
            }
        },

        mounted: function () {
            
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
        }
    }
</script>
