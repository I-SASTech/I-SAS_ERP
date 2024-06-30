<template>
    <modal :modalLarge="true"   :show="show" v-if="show">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="btn-close" v-on:click="close()" ></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-12 text-center" >
                        <iframe :src="iframeSrc"  style="width:74%; height:1000px;" class="img-fluid"/>
                                </div>                              
                </div>
            </div>
           
        </div>
    </modal> 
 
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        props: ['newshow', 'documentpath'],
        mixins: [clickMixin],
        data: function () {
            return {
                show: this.newshow,
                iframeSrc: '',
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
                this.show = false;
            },

           
        },

        created: function () {


            
    
         
                this.isPdf = true;
                this.iframeSrc=this.documentpath


          
        },

        mounted: function () {
            
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
        }
    }
</script>
