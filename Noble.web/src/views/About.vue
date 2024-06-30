<template>
    <div>
        <button @click="printTemplate">Print</button>
        <div class="m-auto" style="width:210mm;height:297mm;border:1px solid black;">
            <div class="page" id="reporting2" style=" border:1px solid blue;">
                <div class="m-auto" id="main" style="border:1px solid black;">

                </div>

            </div>
        </div>
    </div>
</template>
<script>
    export default {
        name: 'about-us',
        data: function () {
            return {
                cvb: ''
            }
        },
        methods: {

            async printTemplate() {

                const prtHtml = document.getElementById('reporting2').innerHTML;
                let stylesHtml = '';
                for (const node of [...document.querySelectorAll('link[rel="stylesheet"], style')]) {
                    stylesHtml += node.outerHTML;
                }
                const WinPrint = window.open('', '', 'left=0,top=0,right=0;bottom=0,width=1000,height=900,toolbar=0,scrollbars=0,status=0');
                WinPrint.document.write(`<!DOCTYPE html>
                    <html>
                    <head>
                        ${stylesHtml}
                    </head>
                    <body>
                        ${prtHtml}
                    </body>
                    </html>`);
                WinPrint.document.close();
                WinPrint.focus();
                WinPrint.print();
                WinPrint.close();

            }
        }
    }


</script>
<style scoped>

    #main {
        width: 200mm;
        height: 287mm;
        margin: 5mm;
    }

    @media print {
        html, body {
            width: 210mm;
            height: 297mm;
            color: black;
        }

        #main {
            width: 200mm;
            height: 287mm;
            margin-top: 5mm;
        }
    }
</style>
