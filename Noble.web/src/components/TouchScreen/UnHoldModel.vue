<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel"> {{ $t('UnHoldModel.UnHoldInvoices') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row ">
                    <div class="form-group col-sm-3 d-grid" v-for="item in saleList" :key="item.id">
                        <!--<button v-on:click="RemoveSale(item.id)" title="Remove Item" style="margin:0;"
                                class="btn btn-danger  btn-sm  btn-icon float-right">
                            <i class="fas fa-trash"></i>
                        </button>-->
                        <button v-on:click="EditSale(item.id)" type="button" class="btn bt-light mb-3 shadow text-start">
                            <div class="row">
                                <div class="col-12">{{ $t('UnHoldModel.RefrenceNo') }} {{item.refrenceNo}}</div>
                                <div class="col-12">{{ $t('UnHoldModel.Invoice') }} {{item.registrationNumber}}</div>
                                <div class="col-12">{{ $t('UnHoldModel.Amount') }} {{item.netAmount}}</div>
                            </div>
                        </button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('UnHoldModel.Close') }}</button>
            </div>
        </div>


    </modal>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        mixins: [clickMixin],
        props: ['show', 'sale'],
        data: function () {
            return {

                saleList: [],
                save: false,
            }
        },
        methods: {
            close: function () {

                this.$emit('close', false);
            },
            RemoveSale: function (id) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟', 
                    text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You will not be able to recover this!' : 'لن تتمكن من استرداد هذا!', 
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!', 
                    closeOnConfirm: false,
                    closeOnCancel: false
                }).then(function (result) {
                    if (result.isConfirmed) {
                        root.$https.get('/Sale/DeleteSale?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                            .then(function (response) {
                                

                                if (response.data != null) {
                                    root.$swal({
                                        title: root.$t('UnHoldModel.SavedSuccessfully'),
                                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Invoice removed successfully' : 'تمت إزالة الفاتورة بنجاح', 
                                        type: 'success',
                                        confirmButtonClass: "btn btn-success",
                                        buttonStyling: false,
                                        icon: 'success',
                                        timer: 1500,
                                        timerProgressBar: true,

                                    }).then(function () {
                                        root.getData('Hold', 1,);
                                    })

                                }
                                else {
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                        text:
                                            (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Invoice not removed successfully' : 'لم تتم إزالة الفاتورة بنجاح ', 
                                        type: 'error',
                                        confirmButtonClass: "btn btn-danger",
                                        icon: 'error',
                                        timer: 1500,
                                        timerProgressBar: true,
                                    });
                                }
                            }).catch(error => {
                                console.log(error)
                                root.$swal.fire(
                                    {
                                        icon: 'error',
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                        text: error.response.data,
                                        showConfirmButton: false,
                                        timer: 5000,
                                        timerProgressBar: true,
                                    });

                            });
                    }
                })
               
            },
            EditSale: function (id) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Sale/SaleDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        
                        if (response.data != null) {
                            root.$emit('EditTOuchInvocie', response.data);
                            root.close();

                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                icon: 'error',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },

            getData: function (status, currentPage) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var branchId = localStorage.getItem('BranchId');

                this.$https.get('/Sale/SaleList?status=' + status + '&pageNumber=' + currentPage + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        root.saleList = response.data.results.sales;
                    });
            },
            //SaveOrigin: function () {


            //    var root = this;

            //    root.close();
            //}
        },
        mounted: function () {

            this.getData('Hold', 1,);

        }
    }
</script>
<style scoped>
    .cardClass:hover {
        background-color: #2d76f7;
        color: white;
        cursor: pointer
    }
</style>
