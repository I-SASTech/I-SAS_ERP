<template>
    <div>
        <div hidden id='rackBarcodePrint' ref="mychildcomponent" >
            <!--HEADER-->
            <div class="row">
                <div class="border border-dark  " style="height:50mm;float:left; width:30%; margin-left:20px; margin-top:10px;"  v-for="(print,index) in printDetails" :key="index" v-bind:style="(index+1)%21 == 0?'margin-bottom:140px;':''">
                    <table style="width:100%">
                        <tr style="border-bottom: 1px solid; ">
                            <td>
                                <p class="text-center" >
                                    <span style="font-size:15px;">{{print.englishName}}</span><br />
                                    <span style="font-size:15px;">{{print.arabicName}}</span><br />
                                    <span style="font-size:19px; font-weight:bold; ">{{currency}}. {{print.salePrice}}</span><br />

                                </p>
                            </td>

                        </tr>
                        <tr >

                            <td style="width: 90%; max-width: 90%; text-align:center">
                                <!--<img :src="'data:image/png;base64,' + image"
                                     style="height: 50px; width: 20%; max-height:50px; max-width:20%; margin-top:10px; padding-left:10px;" />-->
                                <barcode :width="1.5"  :display-value="true" :height="40" v-bind:value="print.barCode"></barcode>
                            </td>

                        </tr>
                    </table>
                </div>
            </div>
            
          

        </div>
    </div>

</template>
<script>
    import Vue3Barcode from 'vue3-barcode'   
   /* import axios from 'axios'*/
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['printDetails', 'printPerLine', 'image'],
        components: {
            'barcode': Vue3Barcode
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                currency:'',
                render: 0,
                logoPath: '',
                pageBreakAfter: 1,
                htmlData: {
                    htmlString: ''
                }
            }
        },

        methods: {
           
            
            printInvoice: function () {
                //// this.$htmlToPaper('purchaseInvoice');
                //this.htmlData.htmlString = this.$refs.mychildcomponent.innerHTML;
                ////  var html1 = this.$refs.mychildcomponent.innerHTML;
                ////  var data = { html: html1 }
                ////
                //alert(this.htmlData.htmlString)
                //var form = new FormData();
                //form.append('htmlString', this.htmlData.htmlString);
                //form.append('NoOfPagesPrint', 0);
                //form.append('PrintType', 'invoice');
                //
                //axios.post('http://127.0.0.1:7015/print/from-pdf', form);
                this.$htmlToPaper('rackBarcodePrint');
            }
        },
        mounted: function () {

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.currency = localStorage.getItem('currency');
            var root = this;
            if (this.printDetails.length > 0) {
               
                setTimeout(function () {
                    root.printInvoice();
                }, 125)
            }
        },

    }
</script>


<style scoped>
    #font11 {
        font-size: 11px;
        line-height: 0;
    }

    #font16 {
        font-size: 16px;
    }
</style>