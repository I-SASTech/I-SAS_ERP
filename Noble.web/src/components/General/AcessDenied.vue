<template>
    <div   v-if="model">
       
    </div>
    <div class="card col-lg-6 col-sm-6 ml-auto mr-auto" v-else>
        <div class="card-body">
            <h1 class="text-center"><code>{{ $t('AcessDenied.AcessDenied') }}</code></h1>
            <hr class="w3-border-white w3-animate-left" style="margin:auto;width:50%">
            <h3 class="w3-center w3-animate-right">{{ $t('AcessDenied.PermissionView') }}</h3>
            <h3 class="text-center">🚫🚫🚫🚫</h3>
            <h6 class="text-center">{{ $t('AcessDenied.ErrorCode') }}</h6>
        </div>
    </div>

</template>
<script>
    
    export default {
        props: ['model'],
        data: function () {
            return {
               
            }
        },
        methods: {
            close: function () {
                //eslint-disable-line
                var root = this;
               

                root.$swal({
                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                    text: "You dont have permission to view this site!",
                    type: 'error',
                    icon: 'error',
                    showConfirmButton: false,
                    timer: 1500,
                    timerProgressBar: true,
                });
            },
        },
        mounted: function () {

            if (this.model) {
                this.close();
            }

        }



       
       

    }

</script>
<style scoped>
  

</style>

